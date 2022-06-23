using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReStore.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using ReStore.Application.IRepositories;
using ReStore.Infrastructure.Repositories;

namespace ReStore.Infrastructure;

public static class InfrastructureServicesRegistration
{
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
                #region Microsoft SQL Server

                services.AddDbContext<ReStoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                #endregion

                #region Repositories

                services.AddScoped<IProductReadRepository, ProductReadRepository>();
                services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

                #endregion

                return services;
        }
}
