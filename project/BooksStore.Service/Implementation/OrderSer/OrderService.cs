using BooksStore.Core.OrderModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Service.OrderSer
{
    public class OrderService : IOrderService
    {
        IOrderRepository OrderRepository { get; set; }
        public OrderService(IOrderRepository orderRepository) => OrderRepository = orderRepository;

        public async Task AddOrderAsync(Order order)
        {
            if(order != null && order != default)
            {
                await OrderRepository.AddOrderAsync(order);
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            if (orderId >= 1)
            {
                return await OrderRepository.GetOrderById(orderId);
            }
            return null;
        }

        public async Task<IEnumerable<Order>> GetOrders(int skip, int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return (await OrderRepository.GetOrders(skip, take) ?? new List<Order>());
            }
            return new List<Order>();
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            if (orderId >= 1)
            {
                var order = await OrderRepository.GetOrderById(orderId);

                if (order != default)
                {
                    await OrderRepository.RemoveOrderAsync(order);
                }
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            if (order != null && order != default)
            {
                await OrderRepository.UpdateOrderAsync(order);
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

        public async Task<IEnumerable<Order>> GetOrdersByAppUserId(string appUserId)
        {
            if (!string.IsNullOrEmpty(appUserId))
            {
                return await OrderRepository.GetOrdersByAppUserId(appUserId);
            }
            return new List<Order>();
        }
    }
}
