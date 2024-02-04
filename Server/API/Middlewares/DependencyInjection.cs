namespace API.Middlewares;

public static class DependencyInjection
{
  public static IServiceCollection AddMiddlewares(this IServiceCollection services)
  {
    services.AddTransient<GlobalExceptionHandlingMiddleware>();

    return services;
  }
}
