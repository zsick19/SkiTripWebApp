using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class MyExceptionHandler(ILogger<MyExceptionHandler> logger) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext,
    Exception exception, CancellationToken cancellationToken)
    {
        
        logger.LogError("Error Loading {message}", exception.Message);
        httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = exception.Message
        });
        return default;
    }
}
