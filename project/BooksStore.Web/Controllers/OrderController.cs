﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderViewModelService _orderService;

        private readonly IBookViewModelService _bookService;

        public OrderController(IOrderViewModelService orderService, IBookViewModelService bookService)
        {
            _orderService = orderService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexOrders(int pageNum = 1)
        {           
            var orders = (await _orderService.GetOrdersAsync(pageNum)).ToList();                                    

            var orderListViewModel = new OrderListViewModel(pageNum, PageSizes.Orders, orders.Count(), orders);            

            return View(orderListViewModel);                      
        }

        [HttpGet]
        public async Task<IActionResult> AddOrder(List<int> booksIds)
        {
            if (booksIds == null | booksIds.Count() == 0)
            {
                return StatusCode(404);
            }

            var books = new List<BookViewModel>();
            foreach(var bookId in booksIds)
            {
                books.Add(await _bookService.GetBookByIdAsync(bookId));
            }

            return View(books);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateModel createModel)
        {               
            await _orderService.AddOrderAsync(createModel);
            
            return RedirectToAction("IndexBooks", "Book");                      
        }

        [HttpPost]
        public async Task<IActionResult> RemoveOrder(int? orderId)
        {
            if (!orderId.HasValue)
            {
                return View(StatusCode(404));
            }

            await _orderService.RemoveOrderAsync(orderId.Value);

            return RedirectToAction(nameof(IndexOrders));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCompleteOrders()
        {
            await _orderService.RemoveCompleteOrderAsync();

            return RedirectToAction(nameof(IndexOrders));
        }
    }
}