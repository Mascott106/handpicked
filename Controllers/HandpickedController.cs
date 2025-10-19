using System.Text.Json;
using HandpickedForJellyfin.Services;
using HandpickedForJellyfin.Models;

namespace HandpickedForJellyfin.Controllers;

/// <summary>
/// API controller for managing handpicked collections.
/// </summary>
public class HandpickedController
{
    private readonly HandpickedCollectionService _collectionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HandpickedController"/> class.
    /// </summary>
    /// <param name="collectionService">The handpicked collection service.</param>
    public HandpickedController(HandpickedCollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    /// <summary>
    /// Gets the handpicked collection configuration.
    /// </summary>
    /// <returns>The collection configuration.</returns>
    public HandpickedCollectionConfig GetConfiguration()
    {
        try
        {
            return _collectionService.GetConfiguration();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting handpicked collection configuration: {ex.Message}");
            return new HandpickedCollectionConfig();
        }
    }

    /// <summary>
    /// Updates the handpicked collection configuration.
    /// </summary>
    /// <param name="config">The new configuration.</param>
    /// <returns>The updated configuration.</returns>
    public HandpickedCollectionConfig UpdateConfiguration(HandpickedCollectionConfig config)
    {
        try
        {
            _collectionService.UpdateConfiguration(config);
            return config;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating handpicked collection configuration: {ex.Message}");
            return config;
        }
    }

    /// <summary>
    /// Gets all handpicked items.
    /// </summary>
    /// <returns>A list of handpicked items.</returns>
    public List<HandpickedItem> GetItems()
    {
        try
        {
            return _collectionService.GetItems();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting handpicked items: {ex.Message}");
            return new List<HandpickedItem>();
        }
    }

    /// <summary>
    /// Gets handpicked items for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>A list of handpicked items visible to the user.</returns>
    public List<HandpickedItem> GetItemsForUser(string userId)
    {
        try
        {
            return _collectionService.GetItemsForUser(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting handpicked items for user: {ex.Message}");
            return new List<HandpickedItem>();
        }
    }

    /// <summary>
    /// Adds an item to the handpicked collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <returns>The added item.</returns>
    public HandpickedItem AddItem(HandpickedItem item)
    {
        try
        {
            _collectionService.AddItem(item);
            return item;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding handpicked item: {ex.Message}");
            return item;
        }
    }

    /// <summary>
    /// Updates an item in the handpicked collection.
    /// </summary>
    /// <param name="itemId">The ID of the item to update.</param>
    /// <param name="item">The updated item.</param>
    /// <returns>The updated item.</returns>
    public HandpickedItem UpdateItem(string itemId, HandpickedItem item)
    {
        try
        {
            if (item.ItemId != itemId)
            {
                Console.WriteLine("Item ID mismatch");
                return item;
            }

            _collectionService.UpdateItem(item);
            return item;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating handpicked item: {ex.Message}");
            return item;
        }
    }

    /// <summary>
    /// Removes an item from the handpicked collection.
    /// </summary>
    /// <param name="itemId">The ID of the item to remove.</param>
    /// <returns>True if successful.</returns>
    public bool RemoveItem(string itemId)
    {
        try
        {
            _collectionService.RemoveItem(itemId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing handpicked item: {ex.Message}");
            return false;
        }
    }
}