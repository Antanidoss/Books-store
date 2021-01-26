﻿using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Managers
{
    public interface ICommentViewModelService
    {
        Task AddCommentAsync(CommentCreateModel model);
        Task<CommentViewModel> GetCommentByIdAsync(int commentId);
        Task<IEnumerable<CommentViewModel>> GetCommentsAsync(int pageNum, int bookId);
        Task RemoveCommentAsync(int commentId);
        Task UpdateCommentAsync(CommentUpdateModel model);
        Task<int> GetCountCommentsAsync(int bookId);
    }
}