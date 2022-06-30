using AutoMapper;
using BooksStore.Common.Exceptions;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Order;
using BooksStore.Services.Implementation.Filters.OrderFilters;
using BooksStore.Services.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.Services.Base
{
    internal sealed class OrderService : IOrderService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        public OrderService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(IEnumerable<int> booksId, string userId)
        {
            var order = new Order(_mapper.Map<IEnumerable<BookOrderJunction>>(booksId), userId, DateTime.Now.AddDays(3));

            await _repositoryFactory.CreateOrderRepository().AddAsync(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders(string appUserId, int skip, int take)
        {
            var orders = (await _repositoryFactory.CreateOrderRepository().GetAsync(skip, take, new OrderByUserIdFilterSpec(appUserId))).ToList() ?? new List<Order>();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await _repositoryFactory.CreateOrderRepository().GetAsync(new OrderByIdFilterSpec(orderId));

            if (order == null)
                throw new NotFoundException(nameof(OrderDTO), order);

            await _repositoryFactory.CreateOrderRepository().RemoveAsync(order);
        }

        public async Task RemoveCompleteOrder(string appUserId)
        {
            var filter = new OrderByUserIdFilterSpec(appUserId);
            int orderCount = await _repositoryFactory.CreateOrderRepository().GetCountAsync(filter);
            var orders = await _repositoryFactory.CreateOrderRepository().GetAsync(0, orderCount, filter);

            foreach (var order in orders)
            {
                if (order.TimeOfDelivery < DateTime.Now)
                    await _repositoryFactory.CreateOrderRepository().RemoveAsync(order);
            }
        }

        public async Task<int> GetCountOrders(string appUserId)
        {
            var filter = new OrderByUserIdFilterSpec(appUserId);

            return await _repositoryFactory.CreateOrderRepository().GetCountAsync(filter);
        }
    }
}