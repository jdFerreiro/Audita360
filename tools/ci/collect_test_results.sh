#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR=$(cd "$(dirname "$0")/../.." && pwd)
RESULTS_DIR="$ROOT_DIR/artifacts/test-results-local"

rm -rf "$RESULTS_DIR"
mkdir -p "$RESULTS_DIR/unit"
mkdir -p "$RESULTS_DIR/integration"

echo "Running unit tests..."
dotnet test "$ROOT_DIR/Audit360.UnitTests/Audit360.UnitTests.csproj" --logger "trx;LogFileName=unit-tests.trx" --results-directory "$RESULTS_DIR/unit"

echo "Running integration tests..."
dotnet test "$ROOT_DIR/Audit360.IntegrationTests/Audit360.IntegrationTests.csproj" --logger "trx;LogFileName=integration-tests.trx" --results-directory "$RESULTS_DIR/integration" || true

# Package results
ZIP_PATH="$ROOT_DIR/artifacts/test-results-local.zip"
rm -f "$ZIP_PATH"

cd "$ROOT_DIR/artifacts"
zip -r "$ZIP_PATH" test-results-local

echo "Test results collected at: $ZIP_PATH"
