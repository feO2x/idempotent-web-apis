using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceB.Contacts.DeleteContact;
using ServiceB.Contacts.GetContact;
using ServiceB.Contacts.GetContacts;
using ServiceB.Contacts.UpsertContact;

namespace ServiceB.Contacts;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(this IServiceCollection services) =>
        services
           .AddGetContactsModule()
           .AddGetContactModule()
           .AddUpsertContactModule()
           .AddDeleteContactModule();

    public static void MapContactsEndpoints(this WebApplication app)
    {
        app.MapGetContactsEndpoint();
        app.MapGetContactEndpoint();
        app.MapUpsertContactEndpoint();
        app.MapDeleteContactEndpoint();
    }
}