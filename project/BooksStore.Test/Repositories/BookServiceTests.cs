using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Services;
using BooksStore.Services.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BooksStore.Tests.Repositories
{
    public class BookServiceTests
    {
        [Fact]
        public async Task BookServiceAddBookAsyncShouldAddBookBooks()
        {
            var repoMock = new Mock<IBookRepository>();
            repoMock.Setup(repo => repo.AddBookAsync(It.IsAny<Book>()));
            repoMock.Setup(repo => repo.GetBookByIdAsync(It.IsAny<int>())).Returns(GetTestBook());
            var mapperMock = await CreateMapperMock();
            var bookService = new BookService(repoMock.Object, mapperMock.Object);
            var bookDto = await GetTestBookDTO();

            await bookService.AddBookAsync(bookDto);

            Assert.Equal(await bookService.GetBookByIdAsync(bookDto.Id), bookDto);
        }
        [Fact]
        public async Task BookServiceGetBooksAsyncShouldReturnBooks()
        {
            var repoMock = new Mock<IBookRepository>();
            repoMock.Setup(repo => repo.GetBooks(0, 3)).Returns(GetTestBooks());
            var mapperMock = await CreateMapperMock();
            var bookService = new BookService(repoMock.Object, mapperMock.Object);

            var result = await bookService.GetBooks(0, 3);

            Assert.Equal(result.Count(), (await GetTestBooks()).Count());
        }
        public async Task<IEnumerable<Book>> GetTestBooks()
        {
            return new List<Book>()
            {
                new Book() { Id = 1, Descriptions="test descr", Title="test title" },
                new Book() { Id = 2, Descriptions="test descr", Title="test title" },
                new Book() { Id = 3, Descriptions="test descr", Title="test title" },
            };
        }
        public async Task<IEnumerable<BookDTO>> GetTestBooksDTO()
        {
            return new List<BookDTO>()
            {
                new BookDTO() { Id = 1, Descriptions="test descr", Title="test title" },
                new BookDTO() { Id = 2, Descriptions="test descr", Title="test title" },
                new BookDTO() { Id = 3, Descriptions="test descr", Title="test title" },
            };
        }
        public async Task<Book> GetTestBook()
        {
            return new Book() 
            {
                Id = 1,
                Descriptions = "test",
                Title = "test",                
            };
        }
        public async Task<BookDTO> GetTestBookDTO()
        {
            return new BookDTO() 
            { 
                Id = 1, 
                Descriptions = "test", 
                Title = "test", 
                CategoryName="test",
                AuthorFirstname = "test",
                AuthorSurname ="test",
            };
        }
        public async Task<Mock<IMapper>> CreateMapperMock()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<BookDTO>>(It.IsAny<IEnumerable<Book>>())).Returns(await GetTestBooksDTO());
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<Book>>(It.IsAny<IEnumerable<BookDTO>>())).Returns(await GetTestBooks());

            return mapperMock;
        }
    }
}
