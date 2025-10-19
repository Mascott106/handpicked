using System.Text.Json;
using HandpickedForJellyfin.Models;

namespace HandpickedForJellyfin.Services;

/// <summary>
/// Service for managing handpicked collections.
/// </summary>
public class HandpickedCollectionService
{
    private readonly string _dataPath;
    private readonly string _configFile;
    private HandpickedCollectionConfig _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="HandpickedCollectionService"/> class.
    /// </summary>
    /// <param name="dataPath">The data path for storing configuration.</param>
    public HandpickedCollectionService(string dataPath)
    {
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
        Console.WriteLine("Handpicked collection configuration updated");
    }

    /// <summary>
    /// Adds an item to the handpicked collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(HandpickedItem item)
    {
        if (_config.Items.Any(i => i.ItemId == item.ItemId))
        {
            Console.WriteLine($"Item {item.ItemId} is already in the handpicked collection");
            return;
        }

        _config.Items.Add(item);
        SaveConfiguration();
        Console.WriteLine($"Added item {item.ItemId} to handpicked collection");
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
            Console.WriteLine($"Removed item {itemId} from handpicked collection");
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
            Console.WriteLine($"Updated item {item.ItemId} in handpicked collection");
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
    /// <param name="userId">The user ID.</param>
    /// <returns>A list of handpicked items visible to the user.</returns>
    public List<HandpickedItem> GetItemsForUser(string userId)
    {
        // For now, return all items - in a real implementation you would filter based on user permissions
        return _config.Items.Where(item => item.IsActive).ToList();
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
            Console.WriteLine($"Error loading handpicked collection configuration: {ex.Message}");
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
            Console.WriteLine($"Error saving handpicked collection configuration: {ex.Message}");
        }
    }
}