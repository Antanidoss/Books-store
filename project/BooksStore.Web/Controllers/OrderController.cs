using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Web.Filters;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
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

            return View(new OrderListViewModel(pageNum, PageSizes.Orders, orders.Count(), orders));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrder(List<int> booksIds)
        {
            var books = new List<BookViewModel>();
            foreach (var bookId in booksIds)
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
        [IdValidationFilter("orderId")]
        public async Task<IActionResult> RemoveOrder(int? orderId)
        {
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