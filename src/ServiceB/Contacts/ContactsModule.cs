using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceB.Contacts.GetContacts;

namespace ServiceB.Contacts;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(this IServiceCollection services)
    {
        return services.AddGetContactsModule();
    }

    public static void MapContactsEndpoints(this WebApplication app)
    {
        app.MapGetContactsEndpoint();
    }
}