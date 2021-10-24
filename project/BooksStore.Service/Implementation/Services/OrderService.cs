using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public OrderService(IRepositoryFactory repositoryFactory, IMapper mapper, ICacheManager cacheManager)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddOrderAsync(IEnumerable<int> booksId, string userId)
        {
            var order = new Order(_mapper.Map<IEnumerable<BookOrderJunction>>(booksId), userId, DateTime.Now.AddDays(3));

            await _repositoryFactory.CreateOrderRepository().AddAsync(order);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetOrderKey(orderId)))
            {
                return _mapper.Map<OrderDTO>(_cacheManager.Get<Order>(CacheKeys.GetOrderKey(orderId)));
            }

            var order = await _repositoryFactory.CreateOrderRepository().GetByIdAsync(orderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(OrderDTO), order);
            }

            _cacheManager.Set<Order>(CacheKeys.GetOrdersKey(order.AppUserId), order, CacheTimes.OrdersCacheTime);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders(string appUserId, int skip, int take)
        {
            var orders = new List<Order>();

            if (_cacheManager.IsSet(CacheKeys.GetOrdersKey(appUserId)))
            {
                orders = _cacheManager.Get<IEnumerable<Order>>(CacheKeys.GetOrdersKey(appUserId)).ToList();
                return _mapper.Map<IEnumerable<OrderDTO>>(orders);
            }

            orders = (await _repositoryFactory.CreateOrderRepository().GetAsync(appUserId, skip, take)).ToList() ?? new List<Order>();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await _repositoryFactory.CreateOrderRepository().GetByIdAsync(orderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(OrderDTO), order);
            }

            await _repositoryFactory.CreateOrderRepository().RemoveAsync(order);
            _cacheManager.Remove(CacheKeys.GetOrdersKey(order.AppUserId));
        }

        public async Task RemoveCompleteOrder(string appUserId)
        {
            int orderCount = await _repositoryFactory.CreateOrderRepository().GetCountAsync(appUserId);
            var orders = await _repositoryFactory.CreateOrderRepository().GetAsync(appUserId, 0, orderCount);

            foreach (var order in orders)
            {
                if (order.TimeOfDelivery < DateTime.Now)
                {
                    await _repositoryFactory.CreateOrderRepository().RemoveAsync(order);
                }
            }

            _cacheManager.Remove(CacheKeys.GetOrdersKey(appUserId));
        }

        public async Task<int> GetCountOrders(string appUserId)
        {
            return await _repositoryFactory.CreateOrderRepository().GetCountAsync(appUserId);
        }
    }
}