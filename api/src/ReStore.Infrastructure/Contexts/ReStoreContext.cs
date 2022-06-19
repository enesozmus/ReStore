using Microsoft.EntityFrameworkCore;
using ReStore.Domain.Configurations;
using ReStore.Domain.Entities;

namespace ReStore.Infrastructure.Contexts;

public class ReStoreContext : DbContext
{
        public ReStoreContext(DbContextOptions options) : base(options) { }

        #region Entities

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.ApplyConfiguration(new CategoryConfiguration());
                modelBuilder.ApplyConfiguration(new ProductConfiguration());

                base.OnModelCreating(modelBuilder);
        }

        #endregion
}
