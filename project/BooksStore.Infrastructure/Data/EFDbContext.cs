using BooksStore.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        public DbSet<Discount> Discounts { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            DbSaveChanges();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void DbSaveChanges()
        {
            var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (var entry in addedEntities)
            {
                if (!(entry.Entity is BaseEntity))
                {
                    return;
                }

                var timeOfCreate = entry.Property(nameof(BaseEntity.TimeOfCreate)).CurrentValue;

                if (timeOfCreate == null || DateTime.Parse(timeOfCreate.ToString()).Year < 2020)
                {
                    entry.Property(nameof(BaseEntity.TimeOfCreate)).CurrentValue = DateTime.UtcNow;
                }
            }

            var updateEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

            foreach (var entry in updateEntities)
            {
                if (!(entry.Entity is BaseEntity))
                {
                    return;
                }

                var timeOfUpdate = entry.Property(nameof(BaseEntity.UpdateTime)).CurrentValue;

                if (timeOfUpdate == null || DateTime.Parse(timeOfUpdate.ToString()).Year < 2020)
                {
                    entry.Property(nameof(BaseEntity.UpdateTime)).CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}