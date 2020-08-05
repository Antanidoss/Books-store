using BooksStore.Core.CategoryModel;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Models.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Converter._Category
{
    public class CategoryConverter : ICategoryConverter
    {
        public CategoryViewModel ConvertToCategoryViewModel(Category category)
        {
            if(category != null)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return categoryViewModel;
            }
            return new CategoryViewModel();
        }

        public IEnumerable<CategoryViewModel> ConvertToCategoryViewModel(IEnumerable<Category> categories)
        {
            if(categories != null && categories.Count() != 0)
            {
                List<CategoryViewModel> categorysViewModel = new List<CategoryViewModel>();
                foreach(var category in categories)
                {
                    categorysViewModel.Add(ConvertToCategoryViewModel(category));
                }
                return categorysViewModel;
            }
            return new List<CategoryViewModel>();
        }
    }
}
