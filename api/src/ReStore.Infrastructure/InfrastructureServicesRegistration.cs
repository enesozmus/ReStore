using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReStore.Application.IRepositories;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;
using ReStore.Infrastructure.Repositories;

namespace ReStore.Infrastructure;

public static class InfrastructureServicesRegistration
{
     public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
     {
          #region Microsoft SQL Server

          services.AddDbContext<ReStoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

          #endregion

          #region Identity Library

          services.AddIdentity<AppUser, AppRole>(options =>
          {
               options.Password.RequiredLength = 8;
               options.Password.RequireNonAlphanumeric = true;
               options.Password.RequireLowercase = true;
               options.Password.RequireUppercase = true;
               options.Password.RequireDigit = true;
               options.User.RequireUniqueEmail = true;
               options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
               //options.SignIn.RequireConfirmedAccount = false;
               //options.SignIn.RequireConfirmedEmail = false;
               //options.SignIn.RequireConfirmedPhoneNumber = false;
               //options.Lockout.MaxFailedAccessAttempts = 3;
               //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

          })
             .AddEntityFrameworkStores<ReStoreContext>();

          #endregion

          #region Repositories

          services.AddScoped<IProductReadRepository, ProductReadRepository>();
          services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

          services.AddScoped<IBasketReadRepository, BasketReadRepository>();
          services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

          #endregion

          return services;
     }
}
