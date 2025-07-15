using Microsoft.Extensions.DependencyInjection;
using ServiceA.HttpAccess;

namespace ServiceA.Contacts.CreateContact;

public static class CreateContactModule
{
    public static IServiceCollection AddCreateContactModule(this IServiceCollection services) =>
        services
           .AddSingleton<CreateContactDtoWithErrorsValidator>()
           .AddChaosClientFactory(HttpCreateContactChaosClient.Create);
}
