﻿using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces.Repositories;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Services
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

            var basket = await _basketRepository.GetByIdAsync(basketId);

            if (basket == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            _cacheManager.Set<Basket>(CacheKeys.GetBasketKey(basket.Id), basket, CacheTimes.BasketCacheTime);
            return _mapper.Map<BasketDTO>(basket);
        }                  

        public async Task AddBasketBookAsync(int basketId, int bookId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);           
            if (basket == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            Book book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book), book);
            }

            var bookBasket = basket.BasketBooks.ToList();
            bookBasket.Add(new BookBasketJunction(basketId, bookId));
            basket.BasketBooks = bookBasket;

            await _basketRepository.UpdateAsync(basket);
            _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
        }

        public async Task RemoveBasketBookAsync(int basketId, int bookId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            Book book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book), book);
            }

            var bookBasket = basket.BasketBooks.ToList();
            BookBasketJunction bookBasketJunction = bookBasket.FirstOrDefault(p => p.BookId == bookId);

            if (bookBasketJunction != default)
            {
                bookBasket.Remove(bookBasketJunction);
                basket.BasketBooks = bookBasket;

                await _basketRepository.UpdateAsync(basket);
                _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
            }            
        }

        public async Task RemoveAllBasketBooksAsync(int basketId)
        {
            Basket basket = new Basket();

            if ((basket = await _basketRepository.GetByIdAsync(basketId)) == null)
            {
                throw new NotFoundException(nameof(Basket), basket);
            }

            var bookBasket = basket.BasketBooks.ToList();
            bookBasket.Clear();
            basket.BasketBooks = bookBasket;

            await _basketRepository.UpdateAsync(basket);
            _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
        }

        public async Task<int> GetBasketBookCount(int basketId)
        {
            return await _basketRepository.GetCountAsync(basketId);
        }
    }
}
