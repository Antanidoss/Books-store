using AutoMapper;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Managers
{
    public class OrderViewModelService : IOrderViewModelService
    {
        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        private readonly ICurrentUser _currentUser;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderViewModelService(IOrderService orderService, IMapper mapper, ICurrentUser currentUser, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _mapper = mapper;
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
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
                OrderBooks =  booksOrder,
                AppUserId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).Id,
                TimeOfDelivery = DateTime.Now.AddDays(3)
            });
        }       

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            return _mapper.Map<OrderViewModel>(await _orderService.GetOrderByIdAsync(orderId));
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
