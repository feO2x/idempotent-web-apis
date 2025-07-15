using Microsoft.Extensions.DependencyInjection;
using Shared.Contacts;

namespace ServiceB.Contacts.GetContacts;

public static class GetContactsModule
{
    public static IServiceCollection AddGetContactsModule(this IServiceCollection services)
    {
        services.AddScoped<IGetContactsClient, EfGetContactsClient>();
        return services;
    }
}