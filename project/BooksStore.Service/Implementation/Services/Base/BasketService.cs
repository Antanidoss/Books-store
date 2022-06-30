using AutoMapper;
using BooksStore.Common.Exceptions;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Basket;
using BooksStore.Services.Implementation.Filters.BasketFilters;
using BooksStore.Services.Implementation.Filters.BookFilters;
using BooksStore.Services.Interfaces.Services.Base;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.Services.Base
{
    internal sealed class BasketService : IBasketService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        public BasketService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async Task<BasketDTO> GetBasketByIdAsync(int basketId)
        {
            var basket = await _repositoryFactory.CreateBasketRepository().GetAsync(new BasketByIdFilterSpec(basketId));
            if (basket == null)
                throw new NotFoundException(nameof(Basket), basket);

            return _mapper.Map<BasketDTO>(basket);
        }

        public async Task AddBasketBookAsync(int basketId, int bookId)
        {
            Basket basket = await _repositoryFactory.CreateBasketRepository().GetAsync(new BasketByIdFilterSpec(basketId));
            if (basket == null)
                throw new NotFoundException(nameof(Basket), basket);

            Book book = await _repositoryFactory.CreateBookRepository().GetAsync(new BookByIdFilterSpec(bookId));
            if (book == null)
                throw new NotFoundException(nameof(Book), book);

            var bookBasket = basket.BasketBooks.ToList();
            bookBasket.Add(new BookBasketJunction(basketId, bookId));
            basket.BasketBooks = bookBasket;

            await _repositoryFactory.CreateBasketRepository().UpdateAsync(basket);
        }

        public async Task RemoveBasketBookAsync(int basketId, int bookId)
        {
            var basketRepository = _repositoryFactory.CreateBasketRepository();
            Basket basket = await basketRepository.GetAsync(new BasketByIdFilterSpec(basketId));
            if (basket == null)
                throw new NotFoundException(nameof(Basket), basket);

            Book book = await _repositoryFactory.CreateBookRepository().GetAsync(new BookByIdFilterSpec(bookId));
            if (book == null)
                throw new NotFoundException(nameof(Book), book);

            var bookBasket = basket.BasketBooks.ToList();
            BookBasketJunction bookBasketJunction = bookBasket.FirstOrDefault(p => p.BookId == bookId);

            if (bookBasketJunction == null)
                return;

            bookBasket.Remove(bookBasketJunction);
            basket.BasketBooks = bookBasket;

            await basketRepository.UpdateAsync(basket);
        }

        public async Task RemoveAllBasketBooksAsync(int basketId)
        {
            Basket basket = new Basket();

            if ((basket = await _repositoryFactory.CreateBasketRepository().GetAsync(new BasketByIdFilterSpec(basketId))) == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            var bookBasket = basket.BasketBooks.ToList();
            bookBasket.Clear();
            basket.BasketBooks = bookBasket;

            await _repositoryFactory.CreateBasketRepository().UpdateAsync(basket);
        }

        public async Task<int> GetBasketBookCount(int basketId)
        {
            return await _repositoryFactory.CreateBasketRepository().GetCountAsync(basketId);
        }
    }
}