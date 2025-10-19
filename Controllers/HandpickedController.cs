using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HandpickedForJellyfin.Services;
using HandpickedForJellyfin.Models;
using Jellyfin.Data.Entities;

namespace HandpickedForJellyfin.Controllers;

/// <summary>
/// API controller for managing handpicked collections.
/// </summary>
[ApiController]
[Route("Handpicked")]
public class HandpickedController : ControllerBase
{
    private readonly ILogger<HandpickedController> _logger;
    private readonly HandpickedCollectionService _collectionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HandpickedController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="collectionService">The handpicked collection service.</param>
    public HandpickedController(ILogger<HandpickedController> logger, HandpickedCollectionService collectionService)
    {
        _logger = logger;
        _collectionService = collectionService;
    }

    /// <summary>
    /// Gets the handpicked collection configuration.
    /// </summary>
    /// <returns>The collection configuration.</returns>
    [HttpGet("Configuration")]
    public ActionResult<HandpickedCollectionConfig> GetConfiguration()
    {
        try
        {
            var config = _collectionService.GetConfiguration();
            return Ok(config);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting handpicked collection configuration");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Updates the handpicked collection configuration.
    /// </summary>
    /// <param name="config">The new configuration.</param>
    /// <returns>The updated configuration.</returns>
    [HttpPost("Configuration")]
    public ActionResult<HandpickedCollectionConfig> UpdateConfiguration([FromBody] HandpickedCollectionConfig config)
    {
        try
        {
            _collectionService.UpdateConfiguration(config);
            return Ok(config);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating handpicked collection configuration");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Gets all handpicked items.
    /// </summary>
    /// <returns>A list of handpicked items.</returns>
    [HttpGet("Items")]
    public ActionResult<List<HandpickedItem>> GetItems()
    {
        try
        {
            var items = _collectionService.GetItems();
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting handpicked items");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Gets handpicked items for the current user.
    /// </summary>
    /// <returns>A list of handpicked items visible to the user.</returns>
    [HttpGet("Items/User")]
    public ActionResult<List<HandpickedItem>> GetItemsForUser()
    {
        try
        {
            // In a real implementation, you would get the current user from the request context
            // For now, we'll return all items
            var items = _collectionService.GetItems();
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting handpicked items for user");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Adds an item to the handpicked collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <returns>The added item.</returns>
    [HttpPost("Items")]
    public ActionResult<HandpickedItem> AddItem([FromBody] HandpickedItem item)
    {
        try
        {
            _collectionService.AddItem(item);
            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding handpicked item");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Updates an item in the handpicked collection.
    /// </summary>
    /// <param name="itemId">The ID of the item to update.</param>
    /// <param name="item">The updated item.</param>
    /// <returns>The updated item.</returns>
    [HttpPut("Items/{itemId}")]
    public ActionResult<HandpickedItem> UpdateItem(string itemId, [FromBody] HandpickedItem item)
    {
        try
        {
            if (item.ItemId != itemId)
            {
                return BadRequest("Item ID mismatch");
            }

            _collectionService.UpdateItem(item);
            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating handpicked item");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Removes an item from the handpicked collection.
    /// </summary>
    /// <param name="itemId">The ID of the item to remove.</param>
    /// <returns>No content on success.</returns>
    [HttpDelete("Items/{itemId}")]
    public ActionResult RemoveItem(string itemId)
    {
        try
        {
            _collectionService.RemoveItem(itemId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing handpicked item");
            return StatusCode(500, "Internal server error");
        }
    }
}

