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
            var connectionString = configuration.GetConnectionString("PostgreDb");
            services.AddHttpContextAccessor();
            services.AddScoped<RlsInterceptor>();

            services.AddDbContext<PostgreDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Infraestructure")));
            return services;
        }
    }
}
