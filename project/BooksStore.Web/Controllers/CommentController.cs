﻿using System.Linq;
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
        private readonly ICommentManager _commentManager;

        private readonly IBookManager _bookManager;

        private readonly ICurrentUser _currentUser;

        public CommentController(ICommentManager commentManager, ICurrentUser currentUser, IBookManager bookManager)
        {
            _currentUser = currentUser;
            _commentManager = commentManager;
            _bookManager = bookManager;
        }

        [HttpGet]
        public async Task<IActionResult> IndexComments(int? bookId, int pageNum = 1)
        {
            var book = await _bookManager.GetBookByIdAsync(bookId.Value);

            var comments = (await _commentManager.GetCommentsByBookId(bookId.Value)).ToList();

            string userId = (await _currentUser.GetCurrentUser(HttpContext)).Id;
            CommentListViewModel bookComment = new CommentListViewModel(book.Title, book.Id, comments.Any(p => p.AppUserId == userId), pageNum,
                PageSizes.Comments, comments.Count(), comments);

            return View(bookComment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentCreateModel model)
        {
            if (!ModelState.IsValid)
            {                                  
                return View(model);
            }

            await _commentManager.AddComment(model);

            return RedirectToAction(nameof(IndexComments), new { bookId = model.BookId });
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveComment(int? commentId, string returnUrl)
        {
            await _commentManager.RemoveComment(commentId.Value);

            return View(returnUrl);
        }
    }
}