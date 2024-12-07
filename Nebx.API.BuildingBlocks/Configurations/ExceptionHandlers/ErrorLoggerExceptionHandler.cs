using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Nebx.API.BuildingBlocks.Configurations.ExceptionHandlers;

public class ErrorLoggerExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ErrorLoggerExceptionHandler> _logger;
    private readonly TimeProvider _timeProvider;

    public ErrorLoggerExceptionHandler(ILogger<ErrorLoggerExceptionHandler> logger, TimeProvider timeProvider)
    {
        _logger = logger;
        _timeProvider = timeProvider;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            "[ERR] Message: {@ExceptionMessage}, Time of occurrence {@Time}", exception.Message,
            _timeProvider.GetLocalNow());

        return true;
    }
}