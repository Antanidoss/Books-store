using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Managers
{
    public interface ICategoryManager 
    {
        Task AddCategoryAsync(CategoryCreateModel categoryDTO);
        Task<CategoryViewModel> GetCategoryById(int categoryId);
        Task RemoveCategoryAsync(int categoryId);
        Task<IEnumerable<CategoryViewModel>> GetCategories(int pageNum);
        Task UpdateCategoryAsync(CategoryUpdateModel categoryDTO);
        Task<int> GetCountCategories();
    }
}
