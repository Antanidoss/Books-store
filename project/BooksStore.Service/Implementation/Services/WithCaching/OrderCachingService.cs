using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Order;
using BooksStore.Services.Interfaces.Services.Base;
using BooksStore.Services.Interfaces.Services.WithCaching;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.Services.WithCaching
{
    internal sealed class OrderCachingService : IOrderCachingService
    {
        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        private readonly IOrderService _orderService;

        public OrderCachingService(IMapper mapper, ICacheManager cacheManager, IOrderService orderService)
        {
            _mapper = mapper;
            _cacheManager = cacheManager;
            _orderService = orderService;
        }

        public async Task AddOrderAsync(IEnumerable<int> booksId, string userId)
        {
            await _orderService.AddOrderAsync(booksId, userId);
        }

        public async Task<int> GetCountOrders(string appUserId)
        {
            return await _orderService.GetCountOrders(appUserId);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders(string appUserId, int skip, int take)
        {
            if (_cacheManager.IsSet(CacheKeys.GetOrdersKey(appUserId)))
            {
                var cachingOrders = _cacheManager.Get<IEnumerable<OrderDTO>>(CacheKeys.GetOrdersKey(appUserId)).ToList();

                return _mapper.Map<IEnumerable<OrderDTO>>(cachingOrders);
            }

            var orders = await _orderService.GetOrders(appUserId, skip, take);
            _cacheManager.Set<IEnumerable<OrderDTO>>(CacheKeys.GetOrdersKey(appUserId), orders, CacheTimes.OrdersCacheTime);

            return orders;
        }

        public async Task RemoveCompleteOrder(string userId)
        {
            await _orderService.RemoveCompleteOrder(userId);

            _cacheManager.Remove(CacheKeys.GetOrdersKey(userId));
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            await _orderService.RemoveOrderAsync(orderId);

            _cacheManager.Remove(CacheKeys.GetOrderKey(orderId));
        }
    }
}
