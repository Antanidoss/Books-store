using AutoMapper;
using BooksStore.Core.BasketModel;
using BooksStore.Core.BookBasketJunctionModel;
using BooksStore.Core.BookModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.CacheOptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Service.BasketSer
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public BasketService(IBasketRepository basketRepository, IBookRepository bookRepository, IMapper mapper, ICacheManager cacheManager)
        {
            _basketRepository = basketRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }       

        public async Task<BasketDTO> GetBasketByIdAsync(int basketId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetBasketKey(basketId)))
            {
                return _mapper.Map<BasketDTO>(_cacheManager.Get<Basket>(CacheKeys.GetBasketKey(basketId)));
            }

            if (basketId <= 0)
            {
                return null;
            }

            var basket = await _basketRepository.GetBasketById(basketId);

            if(basket == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            _cacheManager.Set<Basket>(CacheKeys.GetBasketKey(basket.Id), basket, CacheTimes.BasketCacheTime);
            return _mapper.Map<BasketDTO>(basket);
        }                  

        public async Task AddBasketBookAsync(int basketId, int bookId)
        {
            Basket basket = new Basket();
            Book book = new Book();

            if ((basket = await _basketRepository.GetBasketById(basketId)) == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            if((book = await _bookRepository.GetBookByIdAsync(bookId)) == null)
            {
                throw new NotFoundException(nameof(Book), book);
            }

            var bookBasket = basket.BasketBooks.ToList();
            bookBasket.Add(new BookBasketJunction() { BasketId = basketId, BookId = bookId });
            basket.BasketBooks = bookBasket;

            await _basketRepository.UpdateBasketAsync(basket);
            _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
        }

        public async Task RemoveBasketBookAsync(int basketId, int bookId)
        {
            Basket basket = new Basket();
            Book book = new Book();

            if ((basket = await _basketRepository.GetBasketById(basketId)) == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            if ((book = await _bookRepository.GetBookByIdAsync(bookId)) == null)
            {
                throw new NotFoundException(nameof(Book), book);
            }

            var bookBasket = basket.BasketBooks.ToList();
            BookBasketJunction bookBasketJunction = bookBasket.FirstOrDefault(p => p.BookId == bookId);

            if(bookBasketJunction != default)
            {
                bookBasket.Remove(bookBasketJunction);
                basket.BasketBooks = bookBasket;

                await _basketRepository.UpdateBasketAsync(basket);
                _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
            }            
        }

        public async Task RemoveAllBasketBooksAsync(int basketId)
        {
            Basket basket = new Basket();

            if ((basket = await _basketRepository.GetBasketById(basketId)) == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            var bookBasket = basket.BasketBooks.ToList();
            bookBasket.Clear();
            basket.BasketBooks = bookBasket;

            await _basketRepository.UpdateBasketAsync(basket);
            _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
        }
    }
}
