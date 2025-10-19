#!/bin/bash

# Build script for Handpicked Jellyfin Plugin

echo "Building Handpicked Plugin..."

# Clean previous builds
if [ -d "bin" ]; then
    rm -rf bin
fi

if [ -d "obj" ]; then
    rm -rf obj
fi

# Restore dependencies
echo "Restoring dependencies..."
dotnet restore

if [ $? -ne 0 ]; then
    echo "Failed to restore dependencies"
    exit 1
fi

# Build the project
echo "Building project..."
dotnet build --configuration Release --no-restore

if [ $? -ne 0 ]; then
    echo "Build failed"
    exit 1
fi

# Publish the plugin
echo "Publishing plugin..."
dotnet publish --configuration Release --no-build --output "bin/Release/net6.0/publish"

if [ $? -ne 0 ]; then
    echo "Publish failed"
    exit 1
fi

echo "Build completed successfully!"
echo "Plugin files are in: bin/Release/net6.0/publish/"
echo ""
echo "To install the plugin:"
echo "1. Copy the .dll file to your Jellyfin plugins directory"
echo "2. Restart Jellyfin"
echo "3. Enable the plugin in the Jellyfin dashboard"

