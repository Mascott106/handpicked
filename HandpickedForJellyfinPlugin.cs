using System;
using System.IO;

namespace HandpickedForJellyfin;

/// <summary>
/// Main plugin class for Handpicked Collections.
/// </summary>
public class HandpickedForJellyfinPlugin
{
    /// <summary>
    /// Gets the plugin name.
    /// </summary>
    public string Name => "Handpicked";

    /// <summary>
    /// Gets the plugin description.
    /// </summary>
    public string Description => "Allows administrators to create and display custom curated collections on the front page.";

    /// <summary>
    /// Gets the plugin version.
    /// </summary>
    public string Version => "1.0.0";

    /// <summary>
    /// Gets the plugin author.
    /// </summary>
    public string Author => "Mascott106";

    /// <summary>
    /// Gets the plugin data path.
    /// </summary>
    public string DataPath { get; private set; } = string.Empty;

    /// <summary>
    /// Initializes the plugin.
    /// </summary>
    /// <param name="dataPath">The data path for the plugin.</param>
    public void Initialize(string dataPath)
    {
        DataPath = dataPath;
        Console.WriteLine("Handpicked plugin initialized successfully");
    }

    /// <summary>
    /// Shuts down the plugin.
    /// </summary>
    public void Shutdown()
    {
        Console.WriteLine("Handpicked plugin shutdown");
    }
}

