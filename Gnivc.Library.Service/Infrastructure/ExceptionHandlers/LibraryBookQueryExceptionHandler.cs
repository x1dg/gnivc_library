using Gnivc.Test.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Gnivc.Test.Service.Infrastructure.Middleware;

/// <summary>
/// Обработчик исключения невалидной query на добавление книги
/// </summary>
public class LibraryBookQueryExceptionHandler(ILogger<LibraryBookQueryExceptionHandler> _logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken ct)
    {
        if (exception is not LibraryBookQueryValidationException libraryBookQueryValidationException)
        {
            return false;
        }

        _logger.LogError(
            libraryBookQueryValidationException,
            "Exception occurred: {Message}",
            libraryBookQueryValidationException.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "GNIVC Query for adding book to Library is incorrect. Please check your data.",
            Detail = libraryBookQueryValidationException.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, ct);

        return true;
    }
}