using BooksStore.Core.OrderModel;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.OrderRep
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EFDbContext _context;
        public OrderRepository(EFDbContext context) => this._context = context;

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            Order order = await _context.Orders
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .FirstOrDefaultAsync(p => p.Id == id);

            return order != default ? order : null;
        }        

        public async Task RemoveOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            if(order != null && order != default)
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveRange(IEnumerable<Order> orders)
        {
            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync();         
        }

        public async Task<IEnumerable<Order>> GetOrders(int skip, int take)
        {
            return await _context.Orders
                .Skip(skip)
                .Take(take)
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .ToListAsync();
        }

        public async Task<int> GetCountOrders()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByAppUserId(string appUserId)
        {
            return await _context.Orders
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .Where(p => p.AppUserId == appUserId).ToListAsync();
        }
    }
}
