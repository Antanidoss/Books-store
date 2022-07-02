using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Implementation.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly EFDbContext _context;
        public OrderRepository(EFDbContext context) => _context = context;

        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAsync(int skip, int take, IQueryableFilterSpec<Order> filter)
        {
            return await filter.ApplyFilter(_context.Orders.AsNoTracking())
                .Skip(skip)
                .Take(take)
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .ToListAsync();
        }

        public async Task<Order> GetAsync(IQueryableFilterSpec<Order> filter)
        {
            return await _context.Orders
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .FirstOrDefaultAsync(filter.ToExpression());
        }

        public async Task<int> GetCountAsync(IQueryableFilterSpec<Order> filter)
        {
            return await filter.ApplyFilter(_context.Orders).CountAsync();
        }
    }
}