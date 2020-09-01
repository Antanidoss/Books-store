using BooksStore.Core.BasketModel;
using BooksStore.Core.BookBasketJunctionModel;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.BasketRep
{
    public class BasketRepository : IBasketRepository
    {
        EFDbContext context { get; set; }

        public BasketRepository(EFDbContext context) => this.context = context;

        public async Task AddBasketAsync(Basket basket)
        {
            context.Baskets.Add(basket);
            await context.SaveChangesAsync();
        }

        public async Task<Basket> GetBasketById(int basketId)
        {
            var basket = await context.Baskets
                .Include(p => p.BasketBooks)
                .ThenInclude(p => p.Book)
                .ThenInclude(p => p.Img)
                .FirstOrDefaultAsync(p => p.Id == basketId);       
            
            return basket != default ? basket : null;
        }

        public async Task RemoveBasketAsync(Basket basket)
        {
            context.Baskets.Remove(basket);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBasketAsync(Basket basket)
        {
            context.Baskets.Update(basket);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Basket>> GetBaskets(int skip, int take)
        {
            return await context.Baskets
                .Skip(skip)
                .Take(take)
                .Include(p => p.BasketBooks)
                .ThenInclude(p => p.Book)
                .ToListAsync();
        }
    }
}
