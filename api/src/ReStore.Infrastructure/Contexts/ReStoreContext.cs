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
        public DbSet<Color> Colors { get; set; }

        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.ApplyConfiguration(new CategoryConfiguration());
                modelBuilder.ApplyConfiguration(new ProductConfiguration());
                modelBuilder.ApplyConfiguration(new ColorConfiguration());

                base.OnModelCreating(modelBuilder);
        }

        #endregion
}
