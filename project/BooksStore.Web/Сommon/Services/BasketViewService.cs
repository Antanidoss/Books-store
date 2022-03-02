using AutoMapper;
using BooksStore.Services.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Common.Constants;

namespace BooksStore.Web.Сommon.Services
{
    public class BasketViewService : IBasketViewModelService
    {
        private readonly IBasketService _basketService;

        private readonly IMapper _mapper;

        private readonly ICurrentUser _currentUser;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketViewService(IBasketService basketService, IMapper mapper, ICurrentUser currentUser, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _mapper = mapper;
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddBasketBookAsync(int bookId)
        {
            var curUser = await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext);
            await _basketService.AddBasketBookAsync(curUser.BasketId, bookId);
        }

        public async Task<BasketViewModel> GetBasketAsync(int pageNum)
        {
            var basketId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).BasketId;
            var basket = await _basketService.GetBasketByIdAsync(basketId);
            var pageSize = PageSizes.Basket;
            var books = _mapper.Map<IEnumerable<BookViewModel>>(basket.BasketBooks);

            return new BasketViewModel(pageNum, pageSize, basket.BasketBooks.Count(), basketId, books);
        }

        public async Task RemoveAllBasketBooksAsync()
        {
            var basketId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).BasketId;
            await _basketService.RemoveAllBasketBooksAsync(basketId);
        }

        public async Task RemoveBasketBookAsync(int bookId)
        {
            var basketId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).BasketId;
            await _basketService.RemoveBasketBookAsync(basketId, bookId);
        }
    }
}
