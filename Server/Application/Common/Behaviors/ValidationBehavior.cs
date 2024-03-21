﻿using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }

  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken
  )
  {
    if (!_validators.Any())
    {
      return await next();
    }

    var context = new ValidationContext<TRequest>(request);

    var failures = _validators
      .Select(validator => validator.Validate(context))
      .SelectMany(validationResult => validationResult.Errors)
      .Where(validationFailure => validationFailure is not null)
      .ToList();

    if (failures.Any())
    {
      throw new ValidationException(failures);
    }

    return await next();
  }
}
