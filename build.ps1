# Build script for Handpicked Jellyfin Plugin

Write-Host "Building Handpicked Plugin..." -ForegroundColor Green

# Clean previous builds
if (Test-Path "bin") {
    Remove-Item -Recurse -Force "bin"
}

if (Test-Path "obj") {
    Remove-Item -Recurse -Force "obj"
}

# Restore dependencies
Write-Host "Restoring dependencies..." -ForegroundColor Yellow
dotnet restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to restore dependencies" -ForegroundColor Red
    exit 1
}

# Build the project
Write-Host "Building project..." -ForegroundColor Yellow
dotnet build --configuration Release --no-restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed" -ForegroundColor Red
    exit 1
}

# Publish the plugin
Write-Host "Publishing plugin..." -ForegroundColor Yellow
dotnet publish --configuration Release --no-build --output "bin/Release/net6.0/publish"

if ($LASTEXITCODE -ne 0) {
    Write-Host "Publish failed" -ForegroundColor Red
    exit 1
}

Write-Host "Build completed successfully!" -ForegroundColor Green
Write-Host "Plugin files are in: bin/Release/net6.0/publish/" -ForegroundColor Cyan
Write-Host ""
Write-Host "To install the plugin:" -ForegroundColor Yellow
Write-Host "1. Copy the .dll file to your Jellyfin plugins directory" -ForegroundColor White
Write-Host "2. Restart Jellyfin" -ForegroundColor White
Write-Host "3. Enable the plugin in the Jellyfin dashboard" -ForegroundColor White

