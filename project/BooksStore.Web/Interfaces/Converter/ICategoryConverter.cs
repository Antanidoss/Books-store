using BooksStore.Core.CategoryModel;
using BooksStore.Web.Models.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interface.Converter
{
    public interface ICategoryConverter
    {
        CategoryViewModel ConvertToCategoryViewModel(Category category);
        IEnumerable<CategoryViewModel> ConvertToCategoryViewModel(IEnumerable<Category> categories);
    }
}
