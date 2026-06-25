using domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace order_api.Infrastructure.Filters
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        #region Metodos

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is DomainException domainEx)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Regra de Negócio Violada",
                    Detail = domainEx.Message
                };

                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true;
            }

            return false;
        }

        #endregion
    }
}