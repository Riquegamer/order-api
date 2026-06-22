using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Security.Claims;

namespace Infraestructure.Contexts.Interceptors
{
    public class RlsInterceptor : DbConnectionInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RlsInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task ConnectionOpenedAsync(
            DbConnection connection,
            ConnectionEndEventData eventData,
            CancellationToken cancellationToken = default)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var negocioIdClaim = (user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? user?.FindFirst("sub")?.Value)?
                                  .Replace("\"", "").Trim();

            if (!string.IsNullOrEmpty(negocioIdClaim) && Guid.TryParse(negocioIdClaim, out _))
            {
                using var command = connection.CreateCommand();

                command.CommandText = $"SET app.current_negocio_id = '{negocioIdClaim}';";

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            else
            {
                using var command = connection.CreateCommand();
                command.CommandText = "SET app.current_negocio_id = '';";
                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            await base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
        }
    }
}