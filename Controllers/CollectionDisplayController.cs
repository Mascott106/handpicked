using HandpickedForJellyfin.Services;

namespace HandpickedForJellyfin.Controllers;

/// <summary>
/// API controller for displaying handpicked collections.
/// </summary>
public class CollectionDisplayController
{
    private readonly CollectionDisplayService _displayService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionDisplayController"/> class.
    /// </summary>
    /// <param name="displayService">The collection display service.</param>
    public CollectionDisplayController(CollectionDisplayService displayService)
    {
        _displayService = displayService;
    }

    /// <summary>
    /// Gets the handpicked collection for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The handpicked collection data.</returns>
    public HandpickedCollectionDisplayData? GetCollection(string userId)
    {
        try
        {
            return _displayService.GetCollectionForDisplay(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting handpicked collection for display: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Gets all available collections for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>A list of collection display data.</returns>
    public List<HandpickedCollectionDisplayData> GetAllCollections(string userId)
    {
        try
        {
            return _displayService.GetAllCollectionsForUser(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting all collections for display: {ex.Message}");
            return new List<HandpickedCollectionDisplayData>();
        }
    }

    /// <summary>
    /// Checks if a user has access to handpicked collections.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>True if the user has access.</returns>
    public bool HasAccess(string userId)
    {
        try
        {
            return _displayService.UserHasAccess(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking user access for handpicked collections: {ex.Message}");
            return false;
        }
    }
}