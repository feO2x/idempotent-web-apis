using Microsoft.Extensions.DependencyInjection;

namespace ServiceA.Contacts.Shared;

public static class SharedContactsModule
{
    public static IServiceCollection AddSharedContactsModule(this IServiceCollection services) =>
        services.AddSingleton<ContactIdWithErrorsValidator>();
}
