using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Order> GetByIdAsync(int id)
        {
            Order order = await _context.Orders
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .FirstOrDefaultAsync(p => p.Id == id);

            return order != default ? order : null;
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

        public async Task RemoveAsync(IEnumerable<Order> orders)
        {
            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync(string appUserId)
        {
            return await _context.Orders
                .Where(o => o.AppUserId == appUserId)
                .CountAsync();
        }

        public async Task<IEnumerable<Order>> GetAsync(string appUserId, int skip, int take)
        {
            return await _context.Orders
                .Where(p => p.AppUserId == appUserId)
                .Skip(skip)
                .Take(take)
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .ToListAsync();
        }
    }
}