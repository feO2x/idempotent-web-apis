using Microsoft.Extensions.DependencyInjection;
using ServiceA.HttpAccess;

namespace ServiceA.Contacts.DeleteContact;

public static class DeleteContactModule
{
    public static IServiceCollection AddDeleteContactModule(this IServiceCollection services) =>
        services.AddChaosClientFactory(HttpDeleteContactChaosClient.Create);
}
