using Microsoft.Extensions.DependencyInjection;
using ServiceA.HttpAccess;
using Shared.Contacts;

namespace ServiceA.Contacts.UpdateContact;

public static class UpdateContactModule
{
    public static IServiceCollection AddUpdateContactModule(this IServiceCollection services) =>
        services
           .AddSingleton<UpdateContactWithErrorsValidator>()
           .AddSingleton<ContactValidator>()
           .AddChaosClientFactory(HttpUpdateContactChaosClient.Create);
}
