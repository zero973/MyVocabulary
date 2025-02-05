using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace MyVocabulary.Application.Behaviors;

/// <summary>
/// 
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            var errorList = new ErrorList(failures.Select(f => f.ErrorMessage));

            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var genericType = typeof(TResponse).GetGenericArguments()[0];

                var genericErrorMethod = typeof(Result<>)
                    .MakeGenericType(genericType)
                    .GetMethod(nameof(Result<object>.Error), [typeof(ErrorList)]);

                var genericErrorResult = genericErrorMethod?.Invoke(null, [errorList]);

                return (TResponse)genericErrorResult!;
            }

            return (TResponse)(object)Result.Error(errorList);
        }

        return await next();
    }
}