﻿using API.Mapping;
using Carter;

namespace API;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddCarter();
    services.AddCors();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddMapping();

    services.Configure<RouteOptions>(options =>
    {
      options.LowercaseUrls = true;
      options.LowercaseQueryStrings = true;
    });

    return services;
  }
}