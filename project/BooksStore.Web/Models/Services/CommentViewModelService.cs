using AutoMapper;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
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

        public async Task AddCommentAsync(CommentCreateModel model)
        {
            var commentDto = _mapper.Map<CommentDTO>(model);
            commentDto.AppUserId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).Id;

            await _commentService.AddCommentAsync(commentDto);
        }

        public async Task<CommentViewModel> GetCommentByIdAsync(int commentId)
        {
            var comment = await _commentService.GetCommentById(commentId);

            return _mapper.Map<CommentViewModel>(comment);
        }

        public async Task<IEnumerable<CommentViewModel>> GetCommentsAsync(int pageNum, int bookId)
        {
            if (!PageInfo.PageNumberIsValid(pageNum))
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            int pageSize = PageSizes.Comments;
            int skip = (pageNum - 1) * pageSize;
            var comments = await _commentService.GetComments(skip, pageSize, bookId);

            return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }        

        public async Task<int> GetCountCommentsAsync(int bookId)
        {
            return await _commentService.GetCountComments();
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            await _commentService.RemoveCommentAsync(commentId);
        }

        public async Task UpdateCommentAsync(CommentUpdateModel model)
        {
            var commentDTO = _mapper.Map<CommentDTO>(model);
            await _commentService.UpdateCommentAsync(commentDTO);
        }
    }
}
