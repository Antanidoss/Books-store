﻿using BooksStore.Services.Interfaces.Services.Base;
using BooksStore.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BooksStore.Web.ViewComponents
{
    public class BasketBookCount : ViewComponent
    {
        private readonly IBasketService _basketService;

        private readonly ICurrentUser _currentUser;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketBookCount(IBasketService basketService, ICurrentUser currentUser, IHttpContextAccessor httpContextAccessor) : base()
        {
            _basketService = basketService;
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int basketId = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).BasketId;

            var basketBookCount = (await _basketService.GetBasketBookCount(basketId)).ToString();

            return Content(basketBookCount);
        }
    }
}