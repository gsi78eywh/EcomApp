using LusiTrack.Services;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Extension method to register application-specific services.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ICoffeeCatalogService, CoffeeCatalogService>();
        services.AddScoped<ICartService, SessionCartService>();

        return services;
    }
}
