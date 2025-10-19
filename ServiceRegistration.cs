using Microsoft.Extensions.DependencyInjection;
using HandpickedForJellyfin.Services;

namespace HandpickedForJellyfin;

/// <summary>
/// Service registration for the Handpicked Collections plugin.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Registers plugin services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="dataPath">The data path for plugin storage.</param>
    public static void AddHandpickedServices(this IServiceCollection services, string dataPath)
    {
        services.AddSingleton<HandpickedCollectionService>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<HandpickedCollectionService>>();
            return new HandpickedCollectionService(logger, dataPath);
        });

        services.AddSingleton<CollectionDisplayService>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<CollectionDisplayService>>();
            var collectionService = provider.GetRequiredService<HandpickedCollectionService>();
            return new CollectionDisplayService(logger, collectionService);
        });
    }
}

