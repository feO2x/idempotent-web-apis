using System;
using Microsoft.Extensions.DependencyInjection;
using Shared.CompositionRoot;

namespace ServiceA.HttpAccess;

public static class HttpAccessModule
{
    public static IServiceCollection AddHttpAccess(this IServiceCollection services)
    {
        var serviceBUri = new Uri("https://service-b", UriKind.Absolute);
        services
           .AddServiceDiscovery()
           .AddHttpClientDefaults()
           .AddHttpClient(
                "service-b-client",
                httpClient => httpClient.BaseAddress = serviceBUri
            );
        return services;
    }
}
