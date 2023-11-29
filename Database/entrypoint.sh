#!/usr/bin/env bash

dotnet restore

dotnet build /usr/src/Database/Database.sln --configuration Release --no-incremental

dotnet run \
  --configuration Release \
  --systemConnectionString="$MSSQL_SYSTEM_CONNECTION_STRING" \
  --baseConnectionString="$MSSQL_APP_CONNECTION_STRING" \
  --database="$MSSQL_APP_DATABASE"

dotnet run \
  --configuration Release \
  --systemConnectionString="$MSSQL_SYSTEM_CONNECTION_STRING" \
  --baseConnectionString="$MSSQL_APP_TEST_CONNECTION_STRING" \
  --database="$MSSQL_APP_TEST_DATABASE"

exec "$@"
