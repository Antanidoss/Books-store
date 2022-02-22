using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces.Repositories;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Initializer
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
                if (await authorRepository.GetByNameAsync(author.Firstname, author.Surname) == null)
                {
                    await authorRepository.AddAsync(author);
                }
            }
        }
    }
}