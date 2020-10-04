using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.Index;
using BooksStore.Web.Models.ViewModel.ReadModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    public class CommentController : Controller
    {
        ICommentService CommentService { get; set; }
        IBookService BookService { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IMapper Mapper { get; set; }

        public CommentController(ICommentService commentService, IBookService bookService, ICurrentUser currentUser, IMapper mapper)
        {
            CommentService = commentService;
            BookService = bookService;
            CurrentUser = currentUser;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> IndexComments(int? bookId, int pageNum = 1)
        { 
            BookDTO book = new BookDTO();
            if (pageNum <= 0 && !bookId.HasValue && (book = await BookService.GetBookByIdAsync(bookId.Value)) == null)
            {
                return BadRequest("Некорректные данные в запросе");
            }

            var commentsCache = (await CommentService.GetCommentsByBookId(bookId.Value)).ToList();
                                
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
    }
}