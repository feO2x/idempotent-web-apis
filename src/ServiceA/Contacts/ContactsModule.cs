using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceA.Contacts.CreateContact;
using ServiceA.Contacts.DeleteContact;
using ServiceA.Contacts.GetContact;
using ServiceA.Contacts.GetContacts;
using ServiceA.Contacts.Shared;
using ServiceA.Contacts.UpdateContact;

namespace ServiceA.Contacts;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(this IServiceCollection services) =>
        services
           .AddSharedContactsModule()
           .AddGetContactsModule()
           .AddGetContactModule()
           .AddCreateContactModule()
           .AddUpdateContactModule()
           .AddDeleteContactModule();

    public static void MapContactEndpoints(this WebApplication app)
    {
        app.MapGetContactsEndpoint();
        app.MapGetContactEndpoint();
        app.MapCreateContactEndpoint();
        app.MapUpdateContactEndpoint();
        app.MapDeleteContactEndpoint();
    }
}
