using framework.Models.Commons;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace framework.Filters;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, $"Exception occurred in path: {httpContext.Request.Path}, message: {exception.Message}");
        var defaultObject = new OperationResult();

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
        {
            defaultObject.AddError("exception.message", "We've met an error while trying to execute your request");
        }
        else
        {
            defaultObject.AddError("exception.message", exception.Message);
            if (exception.StackTrace != null)
            {
                defaultObject.AddError("exception.stackTrace", exception.StackTrace);
            }
        }

        defaultObject.Success = false;
        defaultObject.ErrorCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response
            .WriteAsJsonAsync(defaultObject, cancellationToken);

        return true;
    }
}
