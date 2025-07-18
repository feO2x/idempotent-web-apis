using Microsoft.Extensions.DependencyInjection;
using Shared.Contacts;

namespace ServiceB.Contacts.GetContact;

public static class GetContactModule
{
    public static IServiceCollection AddGetContactModule(this IServiceCollection services) =>
        services.AddScoped<IGetContactClient, EfGetContactClient>();
}
