using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentViewModelService _commentService;

        private readonly IBookViewModelService _bookManager;

        private readonly ICurrentUser _currentUser;

        public CommentController(ICommentViewModelService commentService, ICurrentUser currentUser, IBookViewModelService bookManager)
        {
            _currentUser = currentUser;
            _commentService = commentService;
            _bookManager = bookManager;
        }

        [HttpGet]
        public async Task<IActionResult> IndexComments(int? bookId, int pageNum = 1)
        {
            if (!bookId.HasValue)
            {
                return StatusCode(404);
            }

            var book = await _bookManager.GetBookByIdAsync(bookId.Value);

            var comments = (await _commentService.GetCommentsByBookIdAsync(bookId.Value)).ToList();

            string userId = (await _currentUser.GetCurrentUser(HttpContext)).Id;
            CommentListViewModel bookComment = new CommentListViewModel(book.Title, book.Id, comments.Any(p => p.AppUserId == userId), pageNum,
                PageSizes.Comments, comments.Count(), comments);

            return View(bookComment);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentCreateModel model)
        {
            if (!ModelState.IsValid)
            {                                  
                return View(model);
            }

            await _commentService.AddCommentAsync(model);

            return RedirectToAction(nameof(IndexComments), new { bookId = model.BookId });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveComment(int? commentId, string returnUrl)
        {
            if (!commentId.HasValue)
            {
                return StatusCode(404);
            }

            await _commentService.RemoveCommentAsync(commentId.Value);

            return View(returnUrl);
        }
    }
}