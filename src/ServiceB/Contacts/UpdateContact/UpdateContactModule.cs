using Microsoft.Extensions.DependencyInjection;
using Shared.Contacts;

namespace ServiceB.Contacts.UpdateContact;

public static class UpdateContactModule
{
    public static IServiceCollection AddUpdateContactModule(this IServiceCollection services) =>
        services
           .AddScoped<IUpdateContactSession, EfUpdateContactSession>()
           .AddSingleton<ContactValidator>();
}
