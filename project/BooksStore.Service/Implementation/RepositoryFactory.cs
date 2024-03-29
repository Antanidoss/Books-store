﻿using BooksStore.Services.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BooksStore.Services.Implementation
{
    internal sealed class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public IAuthorRepository CreateAuthorRepository() => _serviceProvider.GetService<IAuthorRepository>();

        public IBasketRepository CreateBasketRepository() => _serviceProvider.GetService<IBasketRepository>();

        public IBookRepository CreateBookRepository() => _serviceProvider.GetService<IBookRepository>();

        public ICategoryRepository CreateCategoryRepository() => _serviceProvider.GetService<ICategoryRepository>();

        public ICommentRepository CreateCommentRepository() => _serviceProvider.GetService<ICommentRepository>();

        public IOrderRepository CreateOrderRepository() => _serviceProvider.GetService<IOrderRepository>();
    }
}