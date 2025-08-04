#!/bin/bash

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait until SQL Server is ready
echo "Waiting for SQL Server to start..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'kX57981630!' -Q "SELECT 1" &> /dev/null
do
  sleep 2
done

echo "SQL Server is up. Running init.sql..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'kX57981630!' -i /init.sql

echo "Initialization complete. Bringing SQL Server to foreground..."
wait