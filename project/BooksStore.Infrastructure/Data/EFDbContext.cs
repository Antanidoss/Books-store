using BooksStore.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.Infastructure.Data
{
    public class EFDbContext : IdentityDbContext<AppUser>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BookOrderJunction> BookOrderJunctions { get; set; }
        public DbSet<BookBasketJunction> BookBasketJunctions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Img> Imgs { get; set; }
    }
}
