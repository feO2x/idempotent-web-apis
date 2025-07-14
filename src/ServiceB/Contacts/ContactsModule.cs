using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceB.Contacts.GetContact;
using ServiceB.Contacts.GetContacts;

namespace ServiceB.Contacts;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(this IServiceCollection services) =>
        services
           .AddGetContactsModule()
           .AddGetContactModule();

    public static void MapContactsEndpoints(this WebApplication app)
    {
        app.MapGetContactsEndpoint();
        app.MapGetContactEndpoint();
    }
}