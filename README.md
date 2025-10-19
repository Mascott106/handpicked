# Handpicked for Jellyfin

A Jellyfin plugin that allows administrators to create and display custom curated collections on the front page - essentially a "Handpicked" list of titles, like a staff choice section at a video store.

## Features

- **Admin Configuration**: Easy-to-use web interface for managing handpicked collections
- **Custom Collections**: Create curated lists of your favorite movies, TV shows, and other media
- **Front Page Display**: Collections appear prominently on the Jellyfin home page
- **Flexible Management**: Add, remove, and reorder items with custom descriptions and reasons
- **User Permissions**: Respects Jellyfin's user permission system
- **Persistent Storage**: Collections are saved and persist across server restarts

## Installation

### Option 1: Download Release (Recommended)

1. Download the latest release from the [Releases](https://github.com/Mascott106/handpicked/releases) page
2. Download `HandpickedForJellyfin.dll` from the release assets
3. Place the `.dll` file in your Jellyfin plugins directory:
   - Windows: `%ProgramData%\Jellyfin\Server\plugins`
   - Linux: `/var/lib/jellyfin/plugins`
   - Docker: `/config/plugins`
4. Restart Jellyfin
5. The plugin will appear in your Jellyfin dashboard under "Plugins" as "Handpicked"

### Option 2: Build from Source

1. Clone the repository:
   ```bash
   git clone https://github.com/Mascott106/handpicked.git
   cd handpicked
   ```

2. Build the plugin:
   ```bash
   # Windows
   .\build.ps1
   
   # Linux/macOS
   ./build.sh
   ```

3. Copy the generated `.dll` file from `bin/Release/net6.0/` to your Jellyfin plugins directory
4. Restart Jellyfin

## Configuration

1. Go to your Jellyfin dashboard
2. Navigate to "Plugins" → "Handpicked"
3. Configure your collection settings:
   - **Collection Title**: The name displayed on the front page
   - **Description**: A brief description of your curated collection
   - **Max Items**: Maximum number of items to display
   - **Display Order**: Order relative to other collections
   - **Enable/Disable**: Toggle the collection on/off

## Usage

### Adding Items to Your Collection

1. In the plugin configuration page, you can add items by their Jellyfin item ID
2. To find an item's ID:
   - Go to the item in Jellyfin
   - Look at the URL: `/web/index.html#!/itemdetails.html?id=ITEM_ID`
   - Or use the Jellyfin API to search for items
3. Each item can have:
   - Custom description
   - Handpicked reason (why it's special)
   - Display order within the collection
   - Active/inactive status

### Managing Items

- **Edit**: Modify item details and descriptions
- **Remove**: Remove items from the collection
- **Reorder**: Change the display order of items

### Viewing Collections

Collections will automatically appear on the Jellyfin home page for users who have access to the items. The display respects:
- User permissions
- Parental controls
- Item availability

## API Endpoints

The plugin provides several REST API endpoints for programmatic access:

- `GET /Handpicked/Configuration` - Get collection configuration
- `POST /Handpicked/Configuration` - Update collection configuration
- `GET /Handpicked/Items` - Get all handpicked items
- `GET /Handpicked/Items/User` - Get items visible to current user
- `POST /Handpicked/Items` - Add new item to collection
- `PUT /Handpicked/Items/{itemId}` - Update existing item
- `DELETE /Handpicked/Items/{itemId}` - Remove item from collection

### Display API Endpoints

- `GET /Handpicked/Display/Collection` - Get collection for current user
- `GET /Handpicked/Display/Collection/{userId}` - Get collection for specific user
- `GET /Handpicked/Display/Collections` - Get all collections for current user
- `GET /Handpicked/Display/Access` - Check if current user has access

## Development

### Building from Source

1. Clone the repository:
   ```bash
   git clone https://github.com/Mascott106/handpicked.git
   cd handpicked
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the plugin:
   ```bash
   dotnet build --configuration Release
   ```

4. The compiled plugin will be in `bin/Release/net6.0/`

### Project Structure

```
handpicked/
├── Controllers/           # API controllers
│   ├── HandpickedController.cs
│   └── CollectionDisplayController.cs
├── Models/               # Data models
│   └── HandpickedCollectionConfig.cs
├── Services/             # Business logic
│   ├── HandpickedCollectionService.cs
│   └── CollectionDisplayService.cs
├── web/                  # Frontend configuration page
│   └── index.html
├── HandpickedForJellyfinPlugin.cs  # Main plugin class
├── HandpickedForJellyfin.csproj    # Project file
├── Jellyfin.Plugin.HandpickedForJellyfin.xml  # Plugin manifest
├── ServiceRegistration.cs          # Dependency injection setup
├── build.ps1                       # Windows build script
├── build.sh                        # Linux/macOS build script
├── .gitignore                      # Git ignore rules
└── README.md
```

## Requirements

- Jellyfin Server 10.8.0 or higher
- .NET 6.0 runtime

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License.

## Support

If you encounter any issues or have questions, please:

1. Check the [Issues](https://github.com/Mascott106/handpicked/issues) page
2. Create a new issue with detailed information about your problem
3. Include your Jellyfin version and any relevant error messages

## Changelog

### Version 0.1.0
- Initial release (untested)
- Basic collection management functionality
- Admin configuration interface
- API endpoints for programmatic access
- Front page collection display

