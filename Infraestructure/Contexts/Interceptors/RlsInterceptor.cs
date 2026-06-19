using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Security.Claims;

namespace Infraestructure.Contexts.Interceptors
{
    public class RlsInterceptor : DbCommandInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RlsInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand comand,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            await PrependRlsVariableAsync(comand);
            return await base.ReaderExecutingAsync(comand, eventData, result, cancellationToken);
        }

        public override async ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            await PrependRlsVariableAsync(command);
            return await base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        }

        private Task PrependRlsVariableAsync(DbCommand command)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var negocioIdClaim = (user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? user?.FindFirst("sub")?.Value)?
                                  .Replace("\"", "").Trim();

            if(!string.IsNullOrEmpty(negocioIdClaim) && Guid.TryParse(negocioIdClaim, out _))
            {
                command.CommandText = $"SET LOCAL app.current_negocio_id = '{negocioIdClaim}'; " + command.CommandText;
            }

            return Task.CompletedTask;
        }
    }
}
