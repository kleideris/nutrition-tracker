#!/bin/bash

# ====================================================================================================================================
# This script will generate the .env for the fron and back automatically using the generate.envs.sh script and then run docker-compose
# ====================================================================================================================================


echo "<|> Generating env files..."
./_docker/generate-envs.sh || exit 1


echo "<|> Starting Docker Compose in detached mode......"
docker-compose up --build -d


echo "<|> Waiting for frontend to be ready..."
until curl -s http://localhost:3000 > /dev/null; do
  sleep 2
done

echo "<|> Launching frontend in browser..."
start http://localhost:3000  # Windows-command, for linux you need to run xdg-open instead of start
