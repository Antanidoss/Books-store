using AutoMapper;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Services
{
    public class OrderViewModelService : IOrderViewModelService
    {
        private readonly IOrderService _orderService;

        private readonly IBasketService _basketService;

        private readonly IMapper _mapper;

        private readonly ICurrentUser _currentUser;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderViewModelService(IOrderService orderService, IMapper mapper, ICurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor, IBasketService basketService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
            _basketService = basketService;
        }

        public async Task AddOrderAsync(OrderCreateModel model)
        {
            List<BookDTO> booksOrder = new List<BookDTO>();

            foreach (var bookId in model.BookOrderIds)
            {
                booksOrder.Add(new BookDTO() { Id = bookId });
            }

            await _orderService.AddOrderAsync(new OrderDTO()
            {
                OrderBooks = booksOrder,
                AppUserId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).Id,
                TimeOfDelivery = DateTime.Now.AddDays(3)
            });

            var basketId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).BasketId;
            foreach (var bookId in model.BookOrderIds)
            {
                await _basketService.RemoveBasketBookAsync(basketId, bookId);
            }
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersAsync(int pageNum)
        {
            if (!PageInfo.PageNumberIsValid(pageNum))
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            int pageSize = PageSizes.Orders;
            var curUserId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).Id;
            var orders = await _orderService.GetOrders(curUserId, (pageNum - 1) * pageSize, pageSize);

            return _mapper.Map<IEnumerable<OrderViewModel>>(orders);
        }

        public async Task RemoveCompleteOrderAsync()
        {
            var userId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).Id;
            await _orderService.RemoveCompleteOrder(userId);
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            await _orderService.RemoveOrderAsync(orderId);
        }
    }
}
