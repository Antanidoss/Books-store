using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Core.BookModel;
using BooksStore.Core.CommentModel;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Cache;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.ViewModels.Comment;
using BooksStore.Web.Models.ViewModels.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BooksStore.Web.Controllers
{
    public class CommentController : Controller
    {
        ICommentService CommentService { get; set; }
        ICommentConverter CommentConverter { get; set; }
        IBookService BookService { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IMemoryCache Cache { get; set; }

        public CommentController(ICommentService commentService, ICommentConverter commentConverter, IBookService bookService,
            ICurrentUser currentUser, IMemoryCache cache)
        {
            CommentService = commentService;
            CommentConverter = commentConverter;
            BookService = bookService;
            CurrentUser = currentUser;
            Cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> IndexComments(int? bookId, int pageNum = 1)
        { 
            Book book = new Book();         
            if(pageNum >= 1 && bookId.HasValue && (book = await BookService.GetBookByIdAsync(bookId.Value)) != null)
            {
                int pageSize = 6;
                if (!Cache.TryGetValue(CacheKeys.GetCommentsKey(bookId.Value , pageNum) , out List<Comment> commentsCache))
                {
                    commentsCache = (await CommentService.GetCommentsByBookId(bookId.Value)).ToList();
                    if (commentsCache.Count() != 0)
                    {
                        Cache.Set(CacheKeys.GetCommentsKey(bookId.Value, pageNum), commentsCache, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTime.GetCommentsCacheTime())
                        });
                    }
                }

                string userId = (await CurrentUser.GetCurrentAppUser(HttpContext)).Id;
                BookCommentViewModel bookComment = new BookCommentViewModel() 
                {
                    IndexCommentModel = new IndexViewModel<CommentViewModel>(pageNum , pageSize, await CommentService.GetCountComments(),
                    CommentConverter.ConvertToCommentViewModel(commentsCache)),
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
                Book book = new Book();
                if(bookId.HasValue && (book = await BookService.GetBookByIdAsync(bookId.Value)) != null)
                {
                    string userId = (await CurrentUser.GetCurrentAppUser(HttpContext)).Id;
                    Comment comment = new Comment() { BookId = book.Id, Descriptions = textComment, AppUserId = userId };
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
            Comment removeComment = new Comment();
            if(commentId.HasValue && (removeComment = await CommentService.GetCommentById(commentId.Value)) != null)
            {
                await CommentService.RemoveCommentAsync(removeComment.Id);
            }

            return View(returnUrl);
        }

        private void RemoveCommentCache(int bookId, int pageNum)
        {
            if(Cache.TryGetValue(CacheKeys.GetCommentsKey(bookId, pageNum), out List<Comment> comments))
            {
                Cache.Remove(CacheKeys.GetCommentsKey(bookId, pageNum));
            }
        }
    }
}