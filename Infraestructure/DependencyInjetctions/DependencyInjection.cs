using Infraestructure.Contexts;
using Infraestructure.Contexts.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.DependencyInjetctions
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            #region Conexoes
            
            var connectionString = configuration.GetConnectionString("PostgreDb");
            
            #endregion
            
            services.AddHttpContextAccessor();
            
            #region Interceptadores
            
            services.AddScoped<RlsInterceptor>();

            #endregion

            #region Contestos
            
            services.AddDbContext<PostgreDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Infraestructure")));
            
            #endregion
            
            return services;
        }
    }
}
