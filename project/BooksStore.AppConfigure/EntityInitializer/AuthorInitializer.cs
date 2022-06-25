using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces.Repositories;
using BooksStore.Services.Implementation.Filters.AuthorFilters;
using QueryableFilterSpecification;
using System.Threading.Tasks;

namespace BooksStore.AppConfigure.EntityInitializer
{
    public static class AuthorInitializer
    {
        private static Author[] _baseAuthors = new Author[]
        {
            new Author("Джек", "Лондон"),
            new Author("Агата", "Кристи"),
            new Author("Эрих Мария", "Ремарк"),
        };

        public static async Task InitializeAsync(IAuthorRepository authorRepository)
        {
            foreach (var author in _baseAuthors)
            {
                var filter = new AuthorBySurnameFilterSpec(author.Surname).And(new AuthorByFirstNameFilterSpec(author.Firstname));

                if (await authorRepository.GetAsync(filter) == null)
                    await authorRepository.AddAsync(author);
            }
        }
    }
}