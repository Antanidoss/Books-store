using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces.Repositories;
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
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, ICacheManager cacheManager)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddOrderAsync(OrderDTO orderDTO)
        {
            var books = _mapper.Map<IEnumerable<BookOrderJunction>>(orderDTO.OrderBooks);
            var order = new Order(books, orderDTO.AppUserId, DateTime.Now.AddDays(3));
            await _orderRepository.AddAsync(order);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetOrderKey(orderId)))
            {
                return _mapper.Map<OrderDTO>(_cacheManager.Get<Order>(CacheKeys.GetOrderKey(orderId)));
            }

            var order = await _orderRepository.GetByIdAsync(orderId);

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

            orders = (await _orderRepository.GetAsync(appUserId, skip, take)).ToList() ?? new List<Order>();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(OrderDTO), order);
            }

            await _orderRepository.RemoveAsync(order);
            _cacheManager.Remove(CacheKeys.GetOrdersKey(order.AppUserId));
        }

        public async Task RemoveCompleteOrder(string appUserId)
        {
            int orderCount = await _orderRepository.GetCountAsync(appUserId);
            var orders = await _orderRepository.GetAsync(appUserId, 0, orderCount);

            foreach (var order in orders)
            {
                if (order.TimeOfDelivery < DateTime.Now)
                {
                    await _orderRepository.RemoveAsync(order);
                }
            }

            _cacheManager.Remove(CacheKeys.GetOrdersKey(appUserId));
        }

        public async Task<int> GetCountOrders(string appUserId)
        {
            return await _orderRepository.GetCountAsync(appUserId);
        }
    }
}