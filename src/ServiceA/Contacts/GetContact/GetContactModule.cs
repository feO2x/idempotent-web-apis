using Microsoft.Extensions.DependencyInjection;
using ServiceA.HttpAccess;

namespace ServiceA.Contacts.GetContact;

public static class GetContactModule
{
    public static IServiceCollection AddGetContactModule(this IServiceCollection services) =>
        services.AddChaosClientFactory(HttpGetContactChaosClient.Create);
}
