#!/bin/bash

# ==================================================================================
# This script generates a frontendend and a backend .env file from a root .env file
# ==================================================================================

echo "<|> Starting env generation..."

# set -ex

# Get the absolute path to the directory containing this script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"


# Sanitize .env file
ENV_FILE="$SCRIPT_DIR/../.env"
if command -v dos2unix >/dev/null 2>&1; then
  dos2unix "$ENV_FILE"
else
  sed -i 's/\r$//' "$ENV_FILE"
fi


# Load variables safely
while IFS='=' read -r key value; do
  [[ "$key" =~ ^#.*$ || -z "$key" ]] && continue
  key=$(echo "$key" | xargs)
  value=$(echo "$value" | xargs)
  export "$key=$value"
done < "$ENV_FILE"


# Check required variables
required_vars=(JWT_SECRET SA_PASSWORD DB_CONNECTION VITE_API_URL VITE_PORT)
missing_vars=()

for var in "${required_vars[@]}"; do
  if [ -z "${!var}" ]; then
    missing_vars+=("$var")
  fi
done

if [ ${#missing_vars[@]} -ne 0 ]; then
  echo "<|> Missing required variables in .env:"
  for var in "${missing_vars[@]}"; do
    echo "   - $var"
  done
  exit 1
fi


# Create frontend/.env
frontend_env="$SCRIPT_DIR/../client-frontend/.env"
echo -e "VITE_PORT=$VITE_PORT\nVITE_API_URL=$VITE_API_URL" > "$frontend_env"
echo "<|> $frontend_env created."


# Create backend/.env
backend_env="$SCRIPT_DIR/../server-backend/NutritionTracker.Api/.env"
echo -e "JWT_SECRET=$JWT_SECRET\nSA_PASSWORD=$SA_PASSWORD\nDB_CONNECTION=$DB_CONNECTION" > "$backend_env"
echo "<|> $backend_env created."