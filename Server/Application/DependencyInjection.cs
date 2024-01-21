﻿using System.Reflection;
using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(
      configuration => configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
    );

    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    services.AddValidatorsFromAssembly(
      Assembly.GetExecutingAssembly(),
      ServiceLifetime.Scoped,
      includeInternalTypes: true
    );

    return services;
  }
}
