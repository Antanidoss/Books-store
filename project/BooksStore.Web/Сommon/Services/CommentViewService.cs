using AutoMapper;
using BooksStore.Services.DTO.Comment;
using BooksStore.Services.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Services
{
    public class CommentViewService : ICommentViewModelService
    {
        private readonly ICommentService _commentService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICurrentUser _currentUser;

        private readonly IMapper _mapper;

        public CommentViewService(ICommentService commentService, IHttpContextAccessor httpContextAccessor, ICurrentUser currentUser,
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

        public async Task<IEnumerable<CommentViewModel>> GetCommentsAsync(int pageNum, int bookId)
        {
            int take = PageSizes.Comments;
            int skip = PaginationInfo.GetCountSkipItems(pageNum, take);
            var comments = await _commentService.GetComments(skip, take, bookId);

            return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            await _commentService.RemoveCommentAsync(commentId);
        }
    }
}
