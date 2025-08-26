#!/bin/bash

# ====================================================================================================================================
# This script will generate the .env for the fron and back automatically using the generate.envs.sh script and then run docker-compose
# ====================================================================================================================================


echo "🔧 Generating env files..."
./_docker/generate-envs.sh || exit 1

echo "🚀 Starting Docker Compose..."
docker-compose up --build
