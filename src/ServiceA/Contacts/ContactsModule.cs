using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceA.Contacts.GetContact;
using ServiceA.Contacts.GetContacts;
using ServiceA.Contacts.Shared;

namespace ServiceA.Contacts;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(this IServiceCollection services) =>
        services
           .AddSharedContactsModule()
           .AddGetContactsModule()
           .AddGetContactModule();

    public static void MapContactEndpoints(this WebApplication app)
    {
        app.MapGetContactsEndpoint();
        app.MapGetContactEndpoint();
    }
}
