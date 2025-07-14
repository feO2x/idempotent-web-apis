using Microsoft.Extensions.DependencyInjection;

namespace ServiceB.Contacts.UpdateContact;

public static class UpdateContactModule
{
    public static IServiceCollection AddUpdateContactModule(this IServiceCollection services) =>
        services
           .AddScoped<IUpdateContactSession, EfUpdateContactSession>()
           .AddSingleton<ContactValidator>();
}
