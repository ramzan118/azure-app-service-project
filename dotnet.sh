#!/bin/bash

# Define paths
PROJECT_PATH="/home/ramzan/azure-apps-service-project/backend"
ZIP_PATH="$PROJECT_PATH/publish.zip"
PUBLISH_DIR="$PROJECT_PATH/publish"
NESTED_PUBLISH_DIR="$PROJECT_PATH/bin/Release/net8.0/publish"

# Navigate to project
cd "$PROJECT_PATH" || { echo "❌ Directory $PROJECT_PATH not found"; exit 1; }

# 🧹 Clean up old publish artifacts
echo "🧹 Cleaning previous publish output..."
rm -rf "$PUBLISH_DIR"
rm -f "$ZIP_PATH"
rm -rf "$NESTED_PUBLISH_DIR"

echo "🔧 Restoring .NET dependencies..."
dotnet restore

echo "🚀 Publishing the app in Release mode..."
dotnet publish -c Release -o publish

echo "📦 Creating a flat ZIP of the publish output..."
cd publish || { echo "❌ Publish directory not found"; exit 1; }
zip -r "$ZIP_PATH" .
cd ..

echo "🌐 Deploying to Azure Web App..."
az webapp deploy \
  --resource-group EXPO-RESOURCE-GROUP \
  --name expo-backend-app \
  --src-path "$ZIP_PATH" \
  --type zip

echo "✅ Deployment complete!"

