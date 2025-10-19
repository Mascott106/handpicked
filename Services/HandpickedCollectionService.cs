using System.Text.Json;
using Microsoft.Extensions.Logging;
using Jellyfin.Data.Entities;
using Jellyfin.Data.Enums;

namespace HandpickedForJellyfin.Services;

/// <summary>
/// Service for managing handpicked collections.
/// </summary>
public class HandpickedCollectionService
{
    private readonly ILogger<HandpickedCollectionService> _logger;
    private readonly string _dataPath;
    private readonly string _configFile;
    private HandpickedCollectionConfig _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="HandpickedCollectionService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="dataPath">The data path for storing configuration.</param>
    public HandpickedCollectionService(ILogger<HandpickedCollectionService> logger, string dataPath)
    {
        _logger = logger;
        _dataPath = dataPath;
        _configFile = Path.Combine(_dataPath, "handpicked-collections.json");
        _config = LoadConfiguration();
    }

    /// <summary>
    /// Gets the current handpicked collection configuration.
    /// </summary>
    /// <returns>The handpicked collection configuration.</returns>
    public HandpickedCollectionConfig GetConfiguration()
    {
        return _config;
    }

    /// <summary>
    /// Updates the handpicked collection configuration.
    /// </summary>
    /// <param name="config">The new configuration.</param>
    public void UpdateConfiguration(HandpickedCollectionConfig config)
    {
        _config = config;
        SaveConfiguration();
        _logger.LogInformation("Handpicked collection configuration updated");
    }

    /// <summary>
    /// Adds an item to the handpicked collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(HandpickedItem item)
    {
        if (_config.Items.Any(i => i.ItemId == item.ItemId))
        {
            _logger.LogWarning("Item {ItemId} is already in the handpicked collection", item.ItemId);
            return;
        }

        _config.Items.Add(item);
        SaveConfiguration();
        _logger.LogInformation("Added item {ItemId} to handpicked collection", item.ItemId);
    }

    /// <summary>
    /// Removes an item from the handpicked collection.
    /// </summary>
    /// <param name="itemId">The ID of the item to remove.</param>
    public void RemoveItem(string itemId)
    {
        var item = _config.Items.FirstOrDefault(i => i.ItemId == itemId);
        if (item != null)
        {
            _config.Items.Remove(item);
            SaveConfiguration();
            _logger.LogInformation("Removed item {ItemId} from handpicked collection", itemId);
        }
    }

    /// <summary>
    /// Updates an item in the handpicked collection.
    /// </summary>
    /// <param name="item">The updated item.</param>
    public void UpdateItem(HandpickedItem item)
    {
        var existingItem = _config.Items.FirstOrDefault(i => i.ItemId == item.ItemId);
        if (existingItem != null)
        {
            var index = _config.Items.IndexOf(existingItem);
            _config.Items[index] = item;
            SaveConfiguration();
            _logger.LogInformation("Updated item {ItemId} in handpicked collection", item.ItemId);
        }
    }

    /// <summary>
    /// Gets all handpicked items.
    /// </summary>
    /// <returns>A list of handpicked items.</returns>
    public List<HandpickedItem> GetItems()
    {
        return _config.Items.ToList();
    }

    /// <summary>
    /// Gets handpicked items for a specific user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A list of handpicked items visible to the user.</returns>
    public List<HandpickedItem> GetItemsForUser(User user)
    {
        return _config.Items.Where(item => IsItemVisibleToUser(item, user)).ToList();
    }

    /// <summary>
    /// Checks if an item is visible to a specific user.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <param name="user">The user.</param>
    /// <returns>True if the item is visible to the user.</returns>
    private bool IsItemVisibleToUser(HandpickedItem item, User user)
    {
        // Add your visibility logic here based on user permissions, parental controls, etc.
        return true;
    }

    private HandpickedCollectionConfig LoadConfiguration()
    {
        try
        {
            if (File.Exists(_configFile))
            {
                var json = File.ReadAllText(_configFile);
                var config = JsonSerializer.Deserialize<HandpickedCollectionConfig>(json);
                return config ?? new HandpickedCollectionConfig();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading handpicked collection configuration");
        }

        return new HandpickedCollectionConfig();
    }

    private void SaveConfiguration()
    {
        try
        {
            Directory.CreateDirectory(_dataPath);
            var json = JsonSerializer.Serialize(_config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configFile, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving handpicked collection configuration");
        }
    }
}

