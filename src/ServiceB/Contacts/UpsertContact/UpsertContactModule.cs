using Microsoft.Extensions.DependencyInjection;
using Shared.Contacts;

namespace ServiceB.Contacts.UpsertContact;

public static class UpsertContactModule
{
    public static IServiceCollection AddUpsertContactModule(this IServiceCollection services) =>
        services
           .AddScoped<IUpsertContactClient, EfUpsertContactClient>()
           .AddSingleton<ContactValidator>();
}
