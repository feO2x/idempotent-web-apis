using Microsoft.Extensions.DependencyInjection;

namespace ServiceB.Contacts.GetContacts;

public static class GetContactsModule
{
    public static IServiceCollection AddGetContactsModule(this IServiceCollection services)
    {
        services.AddScoped<IGetContactsClient, EfGetContactsClient>();
        return services;
    }
}