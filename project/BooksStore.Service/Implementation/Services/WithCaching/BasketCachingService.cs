using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Basket;
using BooksStore.Services.Interfaces.Services.Base;
using BooksStore.Services.Interfaces.Services.WithCaching;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.Services.WithCaching
{
    internal sealed class BasketCachingService : IBasketCachingService
    {
        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        private readonly IBasketService _basketService;

        public BasketCachingService(IBasketService basketService, ICacheManager cacheManager, IMapper mapper)
        {
            _basketService = basketService;
            _cacheManager = cacheManager;
            _mapper = mapper;
        }

        public async Task AddBasketBookAsync(int basketId, int bookId)
        {
            await _basketService.AddBasketBookAsync(basketId, bookId);

            _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
        }

        public Task<int> GetBasketBookCount(int basketId)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketDTO> GetBasketByIdAsync(int basketId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetBasketKey(basketId)))
            {
                var cachingBasket = _cacheManager.Get<Basket>(CacheKeys.GetBasketKey(basketId));

                return _mapper.Map<BasketDTO>(cachingBasket);
            }

            var basket = await _basketService.GetBasketByIdAsync(basketId);
            _cacheManager.Set<BasketDTO>(CacheKeys.GetBasketKey(basket.Id), basket, CacheTimes.BasketCacheTime);

            return basket;
        }

        public async Task RemoveAllBasketBooksAsync(int basketId)
        {
            await _basketService.RemoveAllBasketBooksAsync(basketId);

            _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
        }

        public async Task RemoveBasketBookAsync(int basketId, int bookId)
        {
            await _basketService.RemoveBasketBookAsync(basketId, bookId);

            _cacheManager.Remove(CacheKeys.GetBasketKey(basketId));
        }
    }
}
