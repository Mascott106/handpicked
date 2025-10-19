using Jellyfin.Plugin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HandpickedForJellyfin;

/// <summary>
/// Main plugin class for Handpicked Collections.
/// </summary>
public class HandpickedForJellyfinPlugin : IPlugin
{
    private readonly ILogger<HandpickedForJellyfinPlugin> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HandpickedForJellyfinPlugin"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public HandpickedForJellyfinPlugin(ILogger<HandpickedForJellyfinPlugin> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public string Name => "Handpicked";

    /// <inheritdoc />
    public string Description => "Allows administrators to create and display custom curated collections on the front page.";

    /// <inheritdoc />
    public string Version => "1.0.0";

    /// <inheritdoc />
    public string Author => "Your Name";

    /// <inheritdoc />
    public void OnLoad()
    {
        _logger.LogInformation("Handpicked plugin loaded successfully");
    }

    /// <inheritdoc />
    public void OnUnload()
    {
        _logger.LogInformation("Handpicked plugin unloaded");
    }

    /// <inheritdoc />
    public void OnLoadConfiguration()
    {
        _logger.LogInformation("Handpicked plugin configuration loaded");
    }

    /// <inheritdoc />
    public void OnSaveConfiguration()
    {
        _logger.LogInformation("Handpicked plugin configuration saved");
    }
}

