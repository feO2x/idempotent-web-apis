using Microsoft.Extensions.DependencyInjection;
using ServiceA.HttpAccess;

namespace ServiceA.Contacts.GetContacts;

public static class GetContactsModule
{
    public static IServiceCollection AddGetContactsModule(this IServiceCollection services) =>
        services
           .AddSingleton<GetContactsValidator>()
           .AddChaosClientFactory(HttpGetContactsClient.Create);
}
