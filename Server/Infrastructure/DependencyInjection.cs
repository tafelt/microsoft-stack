using Domain.Users;
using Infrastructure.Persistance.DataAccess.SqlServer;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
    services.AddTransient<ISqlConnector, SqlConnector>();
    services.AddScoped<ISettingsService, SettingsService>();
    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }
}
