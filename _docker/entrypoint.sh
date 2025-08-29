#!/bin/bash

# ==============================================================================================================
# This script checks the db connection to make sure its ready then runs the init.sql script to initialize the db
# ==============================================================================================================


# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait until SQL Server is ready
echo "<|> Waiting for SQL Server to start..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" &> /dev/null
do
  sleep 2
done

echo "<|> SQL Server is up. Running init.sql..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i /init.sql

echo "<|> Initialization complete. Bringing SQL Server to foreground..."
wait