using Microsoft.Extensions.DependencyInjection;

namespace ServiceB.Contacts.CreateContact;

public static class CreateContactModule
{
    public static IServiceCollection AddCreateContactModule(this IServiceCollection services) =>
        services.AddScoped<ICreateContactClient, EfCreateContactClient>();
}
