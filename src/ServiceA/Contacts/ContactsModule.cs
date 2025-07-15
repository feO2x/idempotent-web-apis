using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceA.Contacts.GetContacts;

namespace ServiceA.Contacts;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(this IServiceCollection services) =>
        services.AddGetContactsModule();

    public static void MapContactEndpoints(this WebApplication app)
    {
        app.MapGetContactsEndpoint();
    }
}
