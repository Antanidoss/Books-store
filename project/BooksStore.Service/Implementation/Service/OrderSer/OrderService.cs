using AutoMapper;
using BooksStore.Core.OrderModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.OrderSer
{
    public class OrderService : IOrderService
    {
        IOrderRepository OrderRepository { get; set; }
        IMapper Mapper { get; set; }
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            OrderRepository = orderRepository;
            Mapper = mapper;
        }

        public async Task AddOrderAsync(OrderDTO orderDTO)
        {
            if(orderDTO != null && orderDTO != default)
            {             
                await OrderRepository.AddOrderAsync(Mapper.Map<Order>(orderDTO));
            }
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            if (orderId >= 1)
            {
                return Mapper.Map<OrderDTO>(await OrderRepository.GetOrderById(orderId));
            }
            return null;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders(int skip, int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return Mapper.Map<IEnumerable<OrderDTO>>((await OrderRepository.GetOrders(skip, take) ?? new List<Order>()));
            }
            return new List<OrderDTO>();
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            if (orderId >= 1)
            {
                var order = await OrderRepository.GetOrderById(orderId);

                if (order != null)
                {
                    await OrderRepository.RemoveOrderAsync(order);
                }
            }
        }

        public async Task UpdateOrderAsync(OrderDTO orderDTO)
        {
            if (orderDTO != null && orderDTO != default)
            {
                await OrderRepository.UpdateOrderAsync(Mapper.Map<Order>(orderDTO));
            }
        }

        public async Task RemoveCompleteOrder(string appUserId)
        {
            if (!string.IsNullOrEmpty(appUserId))
            {
                foreach (var order in (await OrderRepository.GetOrdersByAppUserId(appUserId)))
                {
                    if (order.TimeOfDelivery < DateTime.Now)
                    {
                        await OrderRepository.RemoveOrderAsync(order);
                    }
                }
            }
        }

        public async Task<int> GetCountOrders()
        {
            return await OrderRepository.GetCountOrders();
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByAppUserId(string appUserId)
        {
            if (!string.IsNullOrEmpty(appUserId))
            {
                return Mapper.Map<IEnumerable<OrderDTO>>(await OrderRepository.GetOrdersByAppUserId(appUserId));
            }
            return new List<OrderDTO>();
        }
    }
}
