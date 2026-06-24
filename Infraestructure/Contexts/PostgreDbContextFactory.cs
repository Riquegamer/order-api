using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Http; // Necessário para instanciar o contexto HTTP vazio
using Infraestructure.Contexts.Interceptors;

namespace Infraestructure.Contexts
{
    public class PostgreDbContextFactory : IDesignTimeDbContextFactory<PostgreDbContext>
    {
        public PostgreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=banco_falso;Username=postgres;Password=senha");

            var httpContextAccessor = new HttpContextAccessor();
            var interceptor = new RlsInterceptor(httpContextAccessor);

            return new PostgreDbContext(optionsBuilder.Options, interceptor);
        }
    }
}