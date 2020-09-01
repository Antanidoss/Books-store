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
        EFDbContext context { get; set; }
        public OrderRepository(EFDbContext context) => this.context = context;

        public async Task AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            Order order = await context.Orders
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .FirstOrDefaultAsync(p => p.Id == id);

            return order != default ? order : null;
        }        

        public async Task RemoveOrderAsync(Order order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            if(order != null && order != default)
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveRange(IEnumerable<Order> orders)
        {
            context.Orders.RemoveRange(orders);
            await context.SaveChangesAsync();         
        }

        public async Task<IEnumerable<Order>> GetOrders(int skip, int take)
        {
            return await context.Orders
                .Skip(skip)
                .Take(take)
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .ToListAsync();
        }

        public async Task<int> GetCountOrders()
        {
            return await context.Orders.CountAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByAppUserId(string appUserId)
        {
            return await context.Orders
                .Include(p => p.OrderBooks)
                .ThenInclude(p => p.Book)
                .Where(p => p.AppUserId == appUserId).ToListAsync();
        }
    }
}
