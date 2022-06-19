using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReStore.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ReStore.Infrastructure;

public static class InfrastructureServicesRegistration
{
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
                #region Microsoft SQL Server

                services.AddDbContext<ReStoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                #endregion


                return services;
        }
}
