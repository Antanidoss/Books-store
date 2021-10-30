﻿using AutoMapper;
using BooksStore.Services.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Services;
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

        public OrderViewModelService(IOrderService orderService, IMapper mapper, ICurrentUser currentUser, IHttpContextAccessor httpContextAccessor, IBasketService basketService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
            _basketService = basketService;
        }

        public async Task AddOrderAsync(OrderCreateModel model)
        {
            await _orderService.AddOrderAsync(model.BookOrderIds, (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).Id);

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
            if (!PaginationInfo.PageNumberIsValid(pageNum))
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            var curUserId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).Id;
            var orders = await _orderService.GetOrders(curUserId, PaginationInfo.GetCountTakeItems(pageNum, PageSizes.Orders), PageSizes.Orders);

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
