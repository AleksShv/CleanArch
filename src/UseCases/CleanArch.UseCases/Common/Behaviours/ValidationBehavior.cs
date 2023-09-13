using FluentValidation;
using MediatR;
using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Common.Behaviours;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IValidatableRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var ctx = new ValidationContext<TRequest>(request);
            var validationTasks = _validators.Select(v => v.ValidateAsync(request, cancellationToken));
            var validationResults = await Task.WhenAll(validationTasks);

            var errors = validationResults
                .Where(r => !r.IsValid)
                .SelectMany(r => r.Errors);

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        return await next();
    }
}
