using Microsoft.Extensions.DependencyInjection;

namespace ServiceB.Contacts.GetContact;

public static class GetContactModule
{
    public static IServiceCollection AddGetContactModule(this IServiceCollection services) =>
        services.AddScoped<IGetContactClient, EfGetContactClient>();
}
