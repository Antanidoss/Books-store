using BooksStore.Core.Entities;
using BooksStore.Services.Interfaces.Repositories;
using BooksStore.Services.Implementation.Filters.CategoryFilters;
using System.Threading.Tasks;

namespace BooksStore.AppConfigure.EntityInitializer
{
    public static class CategoryInitializer
    {
        private static string[] _baseCatrgories = new string[]
        {
            "Программирование",
            "Психология",
            "Фантастика",
            "История",
        };

        public static async Task InitializeAsync(ICategoryRepository categoryRepository)
        {
            foreach (var catgoryName in _baseCatrgories)
            {
                var filter = new CategoryByNameFilterSpec(catgoryName);
                var category = await categoryRepository.GetAsync(filter);

                if (category == null)
                    await categoryRepository.AddAsync(new Category(catgoryName));
            }
        }
    }
}