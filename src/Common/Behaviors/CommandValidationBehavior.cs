using System.Windows.Input;
using Common.Command;
using FluentValidation;
using MediatR;

namespace Common.Behaviors;

public class CommandValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = validators.Select(v=> v.Validate(context))
                        .SelectMany(result => result.Errors)
                        .Where(errors => errors != null).ToList();

        if(failures.Any()){
            throw new ValidationException(failures);
        }

        return await next();

    }
}