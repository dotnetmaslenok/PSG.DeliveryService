﻿using FluentValidation;
using MediatR;

namespace PSG.DeliveryService.Application.PipelineBehaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults =
            await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var validationFailures = 
            validationResults.SelectMany(x => x.Errors).Where(error => error != null).ToList();

        if (validationFailures.Any())
        {
            throw new ValidationException(validationFailures);
        }

        return await next();
    }
}