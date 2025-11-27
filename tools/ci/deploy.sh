#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR=$(cd "$(dirname "$0")/../.." && pwd)
PUBLISH_DIR="$ROOT_DIR/artifacts/publish"
DEPLOY_DIR="$ROOT_DIR/artifacts/deploy_simulated"

if [ ! -d "$PUBLISH_DIR" ]; then
  echo "Publish directory not found: $PUBLISH_DIR"
  echo "Run: dotnet publish ./Audit360.API/Audit360.API.csproj -c Release -o $PUBLISH_DIR"
  exit 1
fi

rm -rf "$DEPLOY_DIR"
mkdir -p "$DEPLOY_DIR"
cp -r "$PUBLISH_DIR"/* "$DEPLOY_DIR/"

echo "Simulated deploy completed: $DEPLOY_DIR"
