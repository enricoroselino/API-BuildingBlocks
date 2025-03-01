﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nebx.API.BuildingBlocks.Shared.Exceptions;

namespace Nebx.API.BuildingBlocks.Configurations.ExceptionHandlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        (string Type, string Title, string Detail, int StatusCode) details = exception switch
        {
            InternalServerException =>
            (
                "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                "Internal Server Error",
                "Something went wrong",
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
            ),
            ValidationException =>
            (
                "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                "Validation Error",
                "one or more validation errors occurred.",
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            BadRequestException =>
            (
                "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                "Bad Request",
                exception.Message,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            NotFoundException =>
            (
                "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                "Not Found",
                exception.Message,
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound
            ),
            _ =>
            (
                "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                "Unknown Error",
                "Something went wrong",
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };

        var problemDetails = new ProblemDetails
        {
            Type = details.Type,
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("TraceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationExceptions)
        {
            var errorsDictionary = validationExceptions.Errors
                .Where(x => x is not null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorsMessages) =>
                        new { Key = propertyName, Messages = errorsMessages.Distinct().ToArray() })
                .ToDictionary(x => x.Key, x => x.Messages);

            problemDetails.Extensions.Add("Errors", errorsDictionary);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}