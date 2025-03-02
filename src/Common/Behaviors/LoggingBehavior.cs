using System.Diagnostics;
using System.Windows.Input;
using Common.Command;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
      logger.LogInformation($"Logging details for {request.GetType().Name}");
       
       var timer = new Stopwatch();
       timer.Start();

       logger.LogInformation($"Starting to execute the request - {request.GetType().Name} request - {request}");

       var response = await next();

       timer.Stop();

       logger.LogInformation($"request completed in {timer.Elapsed.Seconds} seconds with response {response}");

       logger.LogInformation("Passing logs from handler to console");

        return response;


    }
}