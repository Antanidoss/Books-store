using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Managers
{
    public class CommentViewModelService : ICommentViewModelService
    {
        private readonly ICommentService _commentService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICurrentUser _currentUser;

        private readonly IMapper _mapper;

        public CommentViewModelService(ICommentService commentService, IHttpContextAccessor httpContextAccessor, ICurrentUser currentUser, 
            IMapper mapper)
        {
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task AddComment(CommentCreateModel model)
        {
            await _commentService.AddCommentAsync(new CommentDTO()
            {
                AppUserId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).Id,
                Descriptions = model.Descriptions,
                BookId = model.BookId
            });
        }

        public async Task<CommentViewModel> GetCommentById(int commentId)
        {
            if(commentId <= 0)
            {
                throw new ArgumentException("Id не может быть равен или меньше нуля");
            }

            return _mapper.Map<CommentViewModel>(await  _commentService.GetCommentById(commentId));
        }

        public async Task<IEnumerable<CommentViewModel>> GetComments(int pageNum)
        {
            int pageSize = PageSizes.Comments;
            return _mapper.Map<IEnumerable<CommentViewModel>>(await _commentService.GetComments((pageNum - 1) * pageSize, pageSize));
        }

        public async Task<IEnumerable<CommentViewModel>> GetCommentsByBookId(int bookId)
        {
            if (bookId <= 0)
            {
                throw new ArgumentException("Id не может быть равен или меньше нуля");
            }

            return _mapper.Map<IEnumerable<CommentViewModel>>(await _commentService.GetCommentsByBookId(bookId));
        }

        public async Task<int> GetCountComments(int bookId)
        {
            return await _commentService.GetCountComments();
        }

        public async Task RemoveComment(int commentId)
        {
            if (commentId <= 0)
            {
                throw new ArgumentException("Id не может быть равен или меньше нуля");
            }

            await _commentService.RemoveCommentAsync(commentId);
        }

        public async Task UpdateComment(CommentUpdateModel model)
        {
            await _commentService.UpdateCommentAsync(_mapper.Map<CommentDTO>(model));
        }
    }
}
