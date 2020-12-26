using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
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
            if(orderDTO == null)
            {
                throw new ArgumentNullException(nameof(OrderDTO));
            }
            await _orderRepository.AddOrderAsync(_mapper.Map<Order>(orderDTO));
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetOrderKey(orderId)))
            {
                return _mapper.Map<OrderDTO>(_cacheManager.Get<Order>(CacheKeys.GetOrderKey(orderId)));
            }

            if (orderId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");
            }

            var order = await _orderRepository.GetOrderById(orderId);

            if(order == null)
            {
                throw new NotFoundException(nameof(OrderDTO), order);
            }

            _cacheManager.Set<Order>(CacheKeys.GetOrdersKey(order.AppUserId), order, CacheTimes.OrdersCacheTime);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders(int skip, int take)
        {
            if (skip < 0 && take <= 0)
            {
                throw new ArgumentException("Некорректные аргументы skip и take");
            }
            return _mapper.Map<IEnumerable<OrderDTO>>((await _orderRepository.GetOrders(skip, take) ?? new List<Order>()));
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");
            }

            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(OrderDTO), order);
            }

            await _orderRepository.RemoveOrderAsync(order);
            _cacheManager.Remove(CacheKeys.GetOrdersKey(order.AppUserId));
        }

        public async Task UpdateOrderAsync(OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                throw new ArgumentNullException(nameof(OrderDTO));
            }
            await _orderRepository.UpdateOrderAsync(_mapper.Map<Order>(orderDTO));
        }

        public async Task RemoveCompleteOrder(string appUserId)
        {
            if (!string.IsNullOrEmpty(appUserId))
            {
                throw new ArgumentException("id не может быть равен null");
            }

            foreach (var order in (await _orderRepository.GetOrdersByAppUserId(appUserId)))
            {
                if (order.TimeOfDelivery < DateTime.Now)
                {
                    await _orderRepository.RemoveOrderAsync(order);
                }
            }
        }

        public async Task<int> GetCountOrders()
        {
            return await _orderRepository.GetCountOrders();
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByAppUserId(string appUserId)
        {
            if (string.IsNullOrEmpty(appUserId))
            {
                throw new ArgumentException("id не может быть равен null");
            }
            return _mapper.Map<IEnumerable<OrderDTO>>(await _orderRepository.GetOrdersByAppUserId(appUserId));
        }
    }
}
