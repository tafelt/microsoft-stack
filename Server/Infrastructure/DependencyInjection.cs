using Application.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }
}
