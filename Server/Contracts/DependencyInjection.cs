using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Contracts;

public static class DependencyInjection
{
  public static IServiceCollection AddContracts(this IServiceCollection services)
  {
    var config = TypeAdapterConfig.GlobalSettings;

    config.Scan(Assembly.GetExecutingAssembly());
    services.AddSingleton(config);
    services.AddScoped<IMapper, ServiceMapper>();

    return services;
  }
}
