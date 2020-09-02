using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Cache;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModels;
using BooksStore.Web.Models.ViewModels.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BooksStore.Web.Controllers
{
    public class CommentController : Controller
    {
        ICommentService CommentService { get; set; }
        IBookService BookService { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IMemoryCache Cache { get; set; }
        IMapper Mapper { get; set; }

        public CommentController(ICommentService commentService, IBookService bookService, ICurrentUser currentUser, IMemoryCache cache,
            IMapper mapper)
        {
            CommentService = commentService;
            BookService = bookService;
            CurrentUser = currentUser;
            Cache = cache;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> IndexComments(int? bookId, int pageNum = 1)
        { 
            BookDTO book = new BookDTO();         
            if(pageNum >= 1 && bookId.HasValue && (book = await BookService.GetBookByIdAsync(bookId.Value)) != null)
            {
                if (!Cache.TryGetValue(CacheKeys.GetCommentsKey(bookId.Value , pageNum) , out List<CommentDTO> commentsCache))
                {
                    commentsCache = (await CommentService.GetCommentsByBookId(bookId.Value)).ToList();
                    if (commentsCache.Count() != 0)
                    {
                        Cache.Set(CacheKeys.GetCommentsKey(bookId.Value, pageNum), commentsCache, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTimes.CommentsCacheTime)
                        });
                    }
                }

                string userId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;
                BookCommentViewModel bookComment = new BookCommentViewModel() 
                {
                    IndexCommentModel = new IndexViewModel<CommentViewModel>(pageNum , PageSizes.Comments, await CommentService.GetCountComments(),
                    Mapper.Map<IEnumerable<CommentViewModel>>(commentsCache)),
                    BookId = book.Id,
                    BookName = book.Title,
                    UserIsComment = commentsCache.FirstOrDefault(p => p.AppUserId == userId) != default ? true : false
                };

                return View(bookComment);
            }
            return BadRequest("Некорректные данные в запросе");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([Required] string textComment, int? bookId)
        {
            if (ModelState.IsValid)
            {
                BookDTO book = new BookDTO();
                if(bookId.HasValue && (book = await BookService.GetBookByIdAsync(bookId.Value)) != null)
                {
                    string userId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;
                    CommentDTO comment = new CommentDTO() { BookId = book.Id, Descriptions = textComment, AppUserId = userId };
                    await CommentService.AddCommentAsync(comment);

                    RemoveCommentCache(bookId.Value, 1);
                    return RedirectToAction(nameof(IndexComments), new { bookId = bookId });
                }                
                return BadRequest("Некорректные данные в запросе");
            }
            return View(textComment);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveComment(int? commentId, string returnUrl)
        {
            CommentDTO removeComment = new CommentDTO();
            if(commentId.HasValue && (removeComment = await CommentService.GetCommentById(commentId.Value)) != null)
            {
                await CommentService.RemoveCommentAsync(removeComment.Id);
            }

            return View(returnUrl);
        }

        private void RemoveCommentCache(int bookId, int pageNum)
        {
            if(Cache.TryGetValue(CacheKeys.GetCommentsKey(bookId, pageNum), out List<CommentDTO> comments))
            {
                Cache.Remove(CacheKeys.GetCommentsKey(bookId, pageNum));
            }
        }
    }
}