using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ServiceDiscovery;

namespace Shared.CompositionRoot;

public static class Http
{
    public static IServiceCollection AddHttpClientDefaults(this IServiceCollection services) =>
        services
           .ConfigureHttpClientDefaults(builder => builder.AddServiceDiscovery())
           .Configure<ServiceDiscoveryOptions>(
                options =>
                {
                    options.AllowAllSchemes = false;
                    options.AllowedSchemes = ["https"];
                }
            );
}