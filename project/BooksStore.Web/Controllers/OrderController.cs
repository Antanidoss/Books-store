using System.Linq;
using System.Threading.Tasks;
using BooksStore.Web.Interfaces;
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
        private readonly IOrderManager _orderManager;

        private readonly ICurrentUser _currentUser;

        public OrderController(IOrderManager orderManager, ICurrentUser currentUser)
        {
            _orderManager = orderManager;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> IndexOrders(int pageNum = 1)
        {
            string curUserId = (await _currentUser.GetCurrentUser(HttpContext)).Id;
            
            var orders = (await _orderManager.GetOrdersByAppUserId(curUserId)).ToList();                                    

            var orderListViewModel = new OrderListViewModel(pageNum, PageSizes.Orders, orders.Count(), orders);            

            return View(orderListViewModel);                      
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createModel);
            }
                        
            await _orderManager.AddOrderAsync(createModel);
            
            return RedirectToAction("RemoveBasketBooks", "Basket", new { bookIds = createModel.BookOrderIds });                      
        }


        [HttpGet]
        public async Task<IActionResult> RemoveOrder(int? orderId)
        {
            await _orderManager.RemoveOrderAsync(orderId.Value);

            return RedirectToAction(nameof(IndexOrders));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveCompleteOrders()
        {
            await _orderManager.RemoveCompleteOrderAsync();

            return RedirectToAction(nameof(IndexOrders));
        }
    }
}