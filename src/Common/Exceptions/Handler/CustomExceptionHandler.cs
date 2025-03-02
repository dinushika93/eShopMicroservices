using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace Common.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler

{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
       logger.LogError( $"Error: {exception}, Time of occurrence {DateTime.UtcNow}");

        (string Detail, string Title, int StatusCode) detail = exception switch{
            NotFoundException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status404NotFound
            ),
            BadRequestException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status400BadRequest
            ),
            _ => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            )
                    
        };
        
    var problemDetails = new  ProblemDetails {
        Detail = detail.Detail,
        Title = detail.Title,
        Status = detail.StatusCode,
        Instance = httpContext.Request.Path
    };

    if(exception is ValidationException e){
        problemDetails.Extensions.Add("ValidationError", e.Message);
    }

    await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
    return true;

    }
}


