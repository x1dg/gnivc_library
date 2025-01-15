using Gnivc.Test.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Gnivc.Test.Service.Infrastructure.Middleware;

/// <summary>
/// Обработчик исключения невалидной query на добавление книги
/// </summary>
public class LibraryBookUpdateOrCreateQueryExceptionHandler(ILogger<LibraryBookUpdateOrCreateQueryExceptionHandler> _logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken ct)
    {
        if (exception is not LibraryBookUpdateOrCreateQueryValidationException libraryBookUpdateQueryValidationException)
        {
            return false;
        }

        _logger.LogError(
            libraryBookUpdateQueryValidationException,
            "Exception occurred: {Message}",
            libraryBookUpdateQueryValidationException.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "GNIVC Query for updating or creating book in Library is incorrect. Please check your data.",
            Detail = libraryBookUpdateQueryValidationException.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, ct);

        return true;
    }
}