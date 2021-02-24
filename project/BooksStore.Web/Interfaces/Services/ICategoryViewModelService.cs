using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Managers
{
    public interface ICategoryViewModelService 
    {
        Task AddCategoryAsync(CategoryCreateModel categoryDTO);
        Task<CategoryViewModel> GetCategoryById(int categoryId);
        Task RemoveCategoryAsync(int categoryId);
        Task<IEnumerable<CategoryViewModel>> GetCategories(int pageNum);
        Task UpdateCategoryAsync(CategoryUpdateModel categoryDTO);
        Task<int> GetCountCategories();
    }
}
