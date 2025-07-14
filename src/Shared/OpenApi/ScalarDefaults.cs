using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace Shared.OpenApi;

public static class ScalarDefaults
{
    public static void MapDefaultScalarApiReference(this WebApplication app, string title) =>
        app.MapScalarApiReference(options => options.WithTitle(title));
}
