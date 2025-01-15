using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace Gnivc.Test.Service.Infrastructure.Middleware;

/// <summary>
/// Стандартный обработчик исключений
/// </summary>
public class DefaultExceptionHandler(ILogger<DefaultExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
    {
        _logger.LogError(exception, "An unexpected error occurred");

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = exception.GetType().Name,
            Title = "An GNIVC library unexpected error occurred. Please contact out support.",
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        }, cancellationToken: ct);

        return true;
    }
}