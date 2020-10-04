using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.Index;
using BooksStore.Web.Models.ViewModel.ReadModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BooksStore.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        IOrderService OrderService { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IMapper Mapper { get; set; }
        public OrderController(IOrderService orderService, IMemoryCache cache , ICurrentUser currentUser, IMapper mapper)
        {
            OrderService = orderService;
            CurrentUser = currentUser;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> IndexOrders(int pageNum = 1)
        {
            if (pageNum <= 0)
            {
                return BadRequest("Некорректные данные в запросе");
            }
            string curUserId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;
            
            var orders = (await OrderService.GetOrdersByAppUserId(curUserId)).ToList();                                    

            int pageSize = PageSizes.Orders;

            IndexViewModel<OrderViewModel> orderIndexModel = new IndexViewModel<OrderViewModel>(pageNum, pageSize,
                orders.Count(), Mapper.Map<IEnumerable<OrderViewModel>>(orders.Skip((pageNum - 1) * pageSize).Take(pageSize)));

            return View(orderIndexModel);                      
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateModel createModel)
        {
            if (createModel == null && createModel.BookOrderIds.Count() == 0)
            {
                return NotFound();
            }
            string userId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;
            List<BookDTO> booksOrder = new List<BookDTO>();

            foreach(var bookId in createModel.BookOrderIds)
            {
                booksOrder.Add(new BookDTO() { Id = bookId });
            }

            OrderDTO order = new OrderDTO()
            {
                BooksOrder = booksOrder,
                TimeOfDelivery = DateTime.Now.AddDays(3),                   
                AppUserId = userId
            };                
            
            await OrderService.AddOrderAsync(order);
            
            return RedirectToAction("RemoveBasketBooks", "Basket", new { bookIds = order.BooksOrder.Select(p => p.Id) });                      
        }


        [HttpGet]
        public async Task<IActionResult> RemoveOrder(int? orderId)
        {
            OrderDTO order = new OrderDTO();
            if (!orderId.HasValue && (order = await OrderService.GetOrderByIdAsync(orderId.Value)) == null)
            {
                return NotFound();
            }
            string userId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;

            if (order.AppUserId == userId)
            {
                await OrderService.RemoveOrderAsync(order.Id);
            }
            return RedirectToAction(nameof(IndexOrders));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveCompleteOrders()
        {
            string userId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;
            await OrderService.RemoveCompleteOrder(userId);

            return RedirectToAction(nameof(IndexOrders));
        }
    }
}