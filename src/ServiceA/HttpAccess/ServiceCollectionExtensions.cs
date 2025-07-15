using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceA.HttpAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChaosClientFactory<T>(
        this IServiceCollection services,
        CreateChaosClient<T> createChaosClient
    ) =>
        services.AddSingleton<IChaosClientFactory<T>>(
            sp => new DefaultChaosClientFactory<T>(sp.GetRequiredService<IHttpClientFactory>(), createChaosClient)
        );
}
