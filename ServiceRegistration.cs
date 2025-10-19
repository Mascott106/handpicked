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
    /// <param name="dataPath">The data path for plugin storage.</param>
    /// <returns>A tuple containing the registered services.</returns>
    public static (HandpickedCollectionService, CollectionDisplayService) CreateServices(string dataPath)
    {
        var collectionService = new HandpickedCollectionService(dataPath);
        var displayService = new CollectionDisplayService(collectionService);
        
        return (collectionService, displayService);
    }
}