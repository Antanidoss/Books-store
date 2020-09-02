using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Cache;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.CreateModels.Order;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModels.Index;
using BooksStore.Web.Models.ViewModels.Order;
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
        IMemoryCache Cache { get; set; }
        IMapper Mapper { get; set; }
        public OrderController(IOrderService orderService, IMemoryCache cache , ICurrentUser currentUser, IMapper mapper)
        {
            OrderService = orderService;
            Cache = cache;
            CurrentUser = currentUser;
            Mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> IndexOrders(int pageNum = 1)
        {
            if (pageNum >= 1)
            {
                string curUserId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;

                if (!Cache.TryGetValue(CacheKeys.GetOrdersKey(curUserId), out List<OrderDTO> orders))
                {
                    orders = (await OrderService.GetOrdersByAppUserId(curUserId)).ToList();
                    if (orders.Count() != 0)
                    {
                        Cache.Set(CacheKeys.GetOrdersKey(curUserId), orders, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTimes.OrdersCacheTime)
                        });
                    }
                }

                int pageSize = PageSizes.Orders;

                IndexViewModel<OrderViewModel> orderIndexModel = new IndexViewModel<OrderViewModel>(pageNum, pageSize,
                    orders.Count(), Mapper.Map<IEnumerable<OrderViewModel>>(orders.Skip((pageNum - 1) * pageSize).Take(pageSize)));

                return View(orderIndexModel);
            }
            return BadRequest("Некорректные данные в запросе");
        }


        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateModel createModel)
        {
            if (createModel != null && createModel.BookOrderIds.Count() != 0)
            {
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
                RemoveOrderCache(userId);
                
                return RedirectToAction("RemoveBasketBooks", "Basket", new { bookIds = order.BooksOrder.Select(p => p.Id) });
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> RemoveOrder(int? orderId)
        {
            OrderDTO order = new OrderDTO();
            if (orderId.HasValue && (order = await OrderService.GetOrderByIdAsync(orderId.Value)) != null)
            {
                string userId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;

                if (order.AppUserId == userId)
                {
                    await OrderService.RemoveOrderAsync(order.Id);
                    RemoveOrderCache(userId);
                }
                return RedirectToAction(nameof(IndexOrders));
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> RemoveCompleteOrders()
        {
            string userId = (await CurrentUser.GetCurrentUser(HttpContext)).Id;
            await OrderService.RemoveCompleteOrder(userId);

            RemoveOrderCache(userId);

            return RedirectToAction(nameof(IndexOrders));
        }


        private void RemoveOrderCache(string userId)
        {
            if(Cache.TryGetValue(CacheKeys.GetOrdersKey(userId), out List<OrderDTO> orderCache))
            {
                Cache.Remove(CacheKeys.GetOrdersKey(userId));
            }
        }
    }
}