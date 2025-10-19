using HandpickedForJellyfin.Models;

namespace HandpickedForJellyfin.Services;

/// <summary>
/// Service for displaying handpicked collections on the front page.
/// </summary>
public class CollectionDisplayService
{
    private readonly HandpickedCollectionService _collectionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionDisplayService"/> class.
    /// </summary>
    /// <param name="collectionService">The handpicked collection service.</param>
    public CollectionDisplayService(HandpickedCollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    /// <summary>
    /// Gets the handpicked collection for display on the front page.
    /// </summary>
    /// <param name="userId">The user ID requesting the collection.</param>
    /// <returns>The handpicked collection data for display.</returns>
    public HandpickedCollectionDisplayData? GetCollectionForDisplay(string userId)
    {
        try
        {
            var config = _collectionService.GetConfiguration();
            
            if (!config.IsEnabled)
            {
                Console.WriteLine("Handpicked collection is disabled");
                return null;
            }

            var items = _collectionService.GetItemsForUser(userId);
            
            if (!items.Any())
            {
                Console.WriteLine($"No handpicked items available for user {userId}");
                return null;
            }

            // Filter active items and sort by display order
            var activeItems = items
                .Where(item => item.IsActive)
                .OrderBy(item => item.DisplayOrder)
                .Take(config.MaxItems)
                .ToList();

            if (!activeItems.Any())
            {
                Console.WriteLine($"No active handpicked items available for user {userId}");
                return null;
            }

            return new HandpickedCollectionDisplayData
            {
                Title = config.Title,
                Description = config.Description,
                Items = activeItems,
                DisplayOrder = config.DisplayOrder,
                TotalItems = activeItems.Count
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting handpicked collection for display: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Gets all available collections for a user.
    /// </summary>
    /// <param name="userId">The user ID requesting the collections.</param>
    /// <returns>A list of collection display data.</returns>
    public List<HandpickedCollectionDisplayData> GetAllCollectionsForUser(string userId)
    {
        var collections = new List<HandpickedCollectionDisplayData>();
        
        try
        {
            var collection = GetCollectionForDisplay(userId);
            if (collection != null)
            {
                collections.Add(collection);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting all collections for user {userId}: {ex.Message}");
        }

        return collections;
    }

    /// <summary>
    /// Checks if a user has access to handpicked collections.
    /// </summary>
    /// <param name="userId">The user ID to check.</param>
    /// <returns>True if the user has access to handpicked collections.</returns>
    public bool UserHasAccess(string userId)
    {
        try
        {
            // For now, all users have access - in a real implementation you would check permissions
            return !string.IsNullOrEmpty(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking user access for handpicked collections: {ex.Message}");
            return false;
        }
    }
}

/// <summary>
/// Data structure for displaying handpicked collections.
/// </summary>
public class HandpickedCollectionDisplayData
{
    /// <summary>
    /// Gets or sets the collection title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the items in the collection.
    /// </summary>
    public List<HandpickedItem> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the display order of the collection.
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Gets or sets the total number of items in the collection.
    /// </summary>
    public int TotalItems { get; set; }
}