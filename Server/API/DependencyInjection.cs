using API.Mapping;
using API.Middlewares;
using Carter;

namespace API;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddCors();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddMiddlewares();
    services.AddLogging();
    services.AddMapping();

    services.AddCarter(configurator: c =>
    {
      c.WithValidatorLifetime(ServiceLifetime.Scoped);
    });

    services.Configure<RouteOptions>(options =>
    {
      options.LowercaseUrls = true;
      options.LowercaseQueryStrings = true;
    });

    return services;
  }
}
