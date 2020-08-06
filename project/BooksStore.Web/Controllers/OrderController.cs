using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Core.BookOrderJunctionModel;
using BooksStore.Core.OrderModel;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Cache;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.CreateModels.Order;
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
        IOrderConverter OrderConverter { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IMemoryCache Cache { get; set; }
        public OrderController(IOrderService orderService, IOrderConverter orderConverter , IMemoryCache cache , ICurrentUser currentUser)
        {
            OrderService = orderService;
            OrderConverter = orderConverter;
            Cache = cache;
            CurrentUser = currentUser;
        }


        [HttpGet]
        public async Task<IActionResult> IndexOrders(int pageNum = 1)
        {
            if (pageNum >= 1)
            {
                string userId = (await CurrentUser.GetCurrentAppUser(HttpContext)).Id;
                int pageSize = 5;

                if (!Cache.TryGetValue(CacheKeys.GetOrdersKey(userId), out List<Order> orders))
                {
                    orders = (await OrderService.GetOrdersByAppUserId(userId)).ToList();
                    if (orders.Count() != 0)
                    {
                        Cache.Set(CacheKeys.GetOrdersKey(userId), orders, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTime.GetOrdersCacheTime())
                        });
                    }
                }

                IndexViewModel<OrderViewModel> orderIndexModel = new IndexViewModel<OrderViewModel>(pageNum, pageSize,
                    orders.Count(), OrderConverter.ConvertToOrderViewModel(orders.Skip((pageNum - 1) * pageSize).Take(pageSize)));

                return View(orderIndexModel);
            }
            return BadRequest("Некорректные данные в запросе");
        }


        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateModel createModel)
        {
            if (createModel != null && createModel.BookOrderIds.Count() != 0)
            {
                string userId = (await CurrentUser.GetCurrentAppUser(HttpContext)).Id;
                List<BookOrderJunction> bookOrders = new List<BookOrderJunction>();

                foreach(var bookId in createModel.BookOrderIds)
                {
                    bookOrders.Add(new BookOrderJunction() { BookId = bookId });
                }

                Order order = new Order()
                {
                    BookOrders = bookOrders,
                    TimeOfDelivery = DateTime.Now.AddDays(3),                   
                    AppUserId = userId
                };                
                
                await OrderService.AddOrderAsync(order);
                RemoveOrderCache(userId);
                
                return RedirectToAction("RemoveBasketBooks", "Basket", new { bookIds = order.BookOrders.Select(p => p.BookId) });
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> RemoveOrder(int? orderId)
        {
            Order order = new Order();
            if (orderId.HasValue && (order = await OrderService.GetOrderByIdAsync(orderId.Value)) != null)
            {
                string userId = (await CurrentUser.GetCurrentAppUser(HttpContext)).Id;

                if (order != null && order.AppUserId == userId)
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
            string userId = (await CurrentUser.GetCurrentAppUser(HttpContext)).Id;
            await OrderService.RemoveCompleteOrder(userId);

            RemoveOrderCache(userId);

            return RedirectToAction(nameof(IndexOrders));
        }

        private void RemoveOrderCache(string userId)
        {
            if(Cache.TryGetValue(CacheKeys.GetOrdersKey(userId), out List<Order> orderCache))
            {
                Cache.Remove(CacheKeys.GetOrdersKey(userId));
            }
        }
    }
}