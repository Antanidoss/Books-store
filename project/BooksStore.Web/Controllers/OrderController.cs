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
        private readonly IOrderViewModelService _orderService;

        public OrderController(IOrderViewModelService orderService, ICurrentUser currentUser)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexOrders(int pageNum = 1)
        {           
            var orders = (await _orderService.GetOrdersAsync(pageNum)).ToList();                                    

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
                        
            await _orderService.AddOrderAsync(createModel);
            
            return RedirectToAction("RemoveBasketBooks", "Basket", new { bookIds = createModel.BookOrderIds });                      
        }

        [HttpGet]
        public async Task<IActionResult> RemoveOrder(int? orderId)
        {
            if (!orderId.HasValue)
            {
                return View(StatusCode(404));
            }

            await _orderService.RemoveOrderAsync(orderId.Value);

            return RedirectToAction(nameof(IndexOrders));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveCompleteOrders()
        {
            await _orderService.RemoveCompleteOrderAsync();

            return RedirectToAction(nameof(IndexOrders));
        }
    }
}