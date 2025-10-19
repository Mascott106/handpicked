namespace HandpickedForJellyfin.Models;

/// <summary>
/// Configuration for handpicked collections.
/// </summary>
public class HandpickedCollectionConfig
{
    /// <summary>
    /// Gets or sets the collection title.
    /// </summary>
    public string Title { get; set; } = "Handpicked";

    /// <summary>
    /// Gets or sets the collection description.
    /// </summary>
    public string Description { get; set; } = "Curated selection of our favorite titles";

    /// <summary>
    /// Gets or sets whether the collection is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the maximum number of items to display.
    /// </summary>
    public int MaxItems { get; set; } = 20;

    /// <summary>
    /// Gets or sets the display order for the collection.
    /// </summary>
    public int DisplayOrder { get; set; } = 0;

    /// <summary>
    /// Gets or sets the list of handpicked items.
    /// </summary>
    public List<HandpickedItem> Items { get; set; } = new();
}

/// <summary>
/// Represents a handpicked item in the collection.
/// </summary>
public class HandpickedItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the item.
    /// </summary>
    public string ItemId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the item name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the item type (Movie, Series, etc.).
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the custom description for this item.
    /// </summary>
    public string? CustomDescription { get; set; }

    /// <summary>
    /// Gets or sets the custom reason for being handpicked.
    /// </summary>
    public string? HandpickedReason { get; set; }

    /// <summary>
    /// Gets or sets the display order within the collection.
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Gets or sets when the item was added to the collection.
    /// </summary>
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets who added the item to the collection.
    /// </summary>
    public string AddedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the item is currently active in the collection.
    /// </summary>
    public bool IsActive { get; set; } = true;
}

