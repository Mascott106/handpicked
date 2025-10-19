using Microsoft.Extensions.Logging;
using Jellyfin.Data.Entities;
using Jellyfin.Data.Enums;
using HandpickedForJellyfin.Models;

namespace HandpickedForJellyfin.Services;

/// <summary>
/// Service for displaying handpicked collections on the front page.
/// </summary>
public class CollectionDisplayService
{
    private readonly ILogger<CollectionDisplayService> _logger;
    private readonly HandpickedCollectionService _collectionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionDisplayService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="collectionService">The handpicked collection service.</param>
    public CollectionDisplayService(ILogger<CollectionDisplayService> logger, HandpickedCollectionService collectionService)
    {
        _logger = logger;
        _collectionService = collectionService;
    }

    /// <summary>
    /// Gets the handpicked collection for display on the front page.
    /// </summary>
    /// <param name="user">The user requesting the collection.</param>
    /// <returns>The handpicked collection data for display.</returns>
    public HandpickedCollectionDisplayData? GetCollectionForDisplay(User user)
    {
        try
        {
            var config = _collectionService.GetConfiguration();
            
            if (!config.IsEnabled)
            {
                _logger.LogDebug("Handpicked collection is disabled");
                return null;
            }

            var items = _collectionService.GetItemsForUser(user);
            
            if (!items.Any())
            {
                _logger.LogDebug("No handpicked items available for user {UserId}", user.Id);
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
                _logger.LogDebug("No active handpicked items available for user {UserId}", user.Id);
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
            _logger.LogError(ex, "Error getting handpicked collection for display");
            return null;
        }
    }

    /// <summary>
    /// Gets all available collections for a user.
    /// </summary>
    /// <param name="user">The user requesting the collections.</param>
    /// <returns>A list of collection display data.</returns>
    public List<HandpickedCollectionDisplayData> GetAllCollectionsForUser(User user)
    {
        var collections = new List<HandpickedCollectionDisplayData>();
        
        try
        {
            var collection = GetCollectionForDisplay(user);
            if (collection != null)
            {
                collections.Add(collection);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all collections for user {UserId}", user.Id);
        }

        return collections;
    }

    /// <summary>
    /// Checks if a user has access to handpicked collections.
    /// </summary>
    /// <param name="user">The user to check.</param>
    /// <returns>True if the user has access to handpicked collections.</returns>
    public bool UserHasAccess(User user)
    {
        try
        {
            // Add your access control logic here
            // For example, check if user is admin or has specific permissions
            return user.HasPermission(PermissionKind.IsAdministrator) || 
                   user.HasPermission(PermissionKind.EnableAllDevices);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking user access for handpicked collections");
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

