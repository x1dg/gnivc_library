using Gnivc.Test.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Gnivc.Test.Service.Infrastructure.Middleware;

/// <summary>
/// Обработчик исключения не найденой книги
/// </summary>
public class LibraryBookNotFoundExceptionHandler(ILogger<LibraryBookNotFoundExceptionHandler> _logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken ct)
    {
        if (exception is not LibraryBookNotFoundException libraryBookNotFoundException)
        {
            return false;
        }

        _logger.LogError(
            libraryBookNotFoundException,
            "Exception occurred: {Message}",
            libraryBookNotFoundException.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "GNIVC Library book was not found. Please check your data or contact 2nd line.",
            Detail = libraryBookNotFoundException.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, ct);

        return true;
    }
}