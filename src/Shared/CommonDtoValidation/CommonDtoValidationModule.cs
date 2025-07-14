using Light.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.CommonDtoValidation;

public static class CommonDtoValidationModule
{
    public static IServiceCollection AddCommonDtoValidation(this IServiceCollection services)
    {
        return services
           .AddSingleton<IValidationContextFactory>(_ => ValidationContextFactory.Instance)
           .AddSingleton<ValidationContext>(_ => ValidationContextFactory.CreateContext())
           .AddSingleton<PagingValidator>();
    }
}