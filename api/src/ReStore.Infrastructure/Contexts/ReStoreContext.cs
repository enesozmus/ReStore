using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReStore.Domain.Configurations;
using ReStore.Domain.Entities;

namespace ReStore.Infrastructure.Contexts;

public class ReStoreContext : IdentityDbContext<AppUser, AppRole, int>
{
     public ReStoreContext(DbContextOptions options) : base(options) { }

     #region Entities

     public DbSet<Product> Products { get; set; }
     public DbSet<Basket> Baskets { get; set; }
     public DbSet<Order> Orders { get; set; }

     #endregion

     #region SaveChangesAsync

     public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
     {
          foreach (var entry in ChangeTracker.Entries<BaseEntity>())
          {
               switch (entry.State)
               {
                    case EntityState.Added:
                         entry.Entity.CreatedDate = DateTime.Now;

                         break;
                    case EntityState.Modified:
                         entry.Entity.LastModifiedDate = DateTime.Now;
                         break;
               }
          }
          return base.SaveChangesAsync(cancellationToken);
     }

     #endregion

     #region OnModelCreating

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          //modelBuilder.ApplyConfiguration(new CategoryConfiguration());
          modelBuilder.ApplyConfiguration(new ProductConfiguration());
          //modelBuilder.ApplyConfiguration(new ColorConfiguration());
          modelBuilder.ApplyConfiguration(new AppUserConfiguration());
          modelBuilder.ApplyConfiguration(new AppRoleConfiguration());

          this.SeedUserRoles(modelBuilder);

          base.OnModelCreating(modelBuilder);
     }

     private void SeedUserRoles(ModelBuilder builder)
     {
          builder.Entity<IdentityUserRole<int>>().HasData(
              new IdentityUserRole<int>() { RoleId = 2, UserId = 1 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 2 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 3 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 4 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 5 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 6 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 7 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 8 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 9 },
              new IdentityUserRole<int>() { RoleId = 1, UserId = 10 }
              );
     }

     #endregion
}
