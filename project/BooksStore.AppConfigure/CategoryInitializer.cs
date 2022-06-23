using BooksStore.Infastructure.Interfaces.Repositories;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Initializer
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
                var category = await categoryRepository.GetByNameAsync(catgoryName);

                if (category == null)
                {
                    await categoryRepository.AddAsync(new Core.Entities.Category(catgoryName));
                }
            }
        }
    }
}