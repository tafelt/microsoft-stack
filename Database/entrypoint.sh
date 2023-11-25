#!/bin/bash -e

export DATA_SOURCE="mssql"
export APP_DATABASE="appdb"
export APP_TEST_DATABASE="${APP_DATABASE}_test"
export USER_ID="sa"
export PASSWORD=$MSSQL_SA_PASSWORD

export SYSTEM_CONNECTION_STRING="Data Source=${DATA_SOURCE};User ID=${USER_ID};Password=${PASSWORD};"
export APP_CONNECTION_STRING="Data Source=${DATA_SOURCE};Database=${APP_DATABASE};User ID=${USER_ID};Password=${PASSWORD};"
export APP_TEST_CONNECTION_STRING="Data Source=${DATA_SOURCE};Database=${APP_TEST_DATABASE};User ID=${USER_ID};Password=${PASSWORD};"

dotnet restore

dotnet build /usr/src/Database/Database.sln --configuration Release --no-incremental

dotnet run \
  --configuration Release \
  --systemConnectionString="$SYSTEM_CONNECTION_STRING" \
  --baseConnectionString="$APP_CONNECTION_STRING" \
  --database="$APP_DATABASE"

dotnet run \
  --configuration Release \
  --systemConnectionString="$SYSTEM_CONNECTION_STRING" \
  --baseConnectionString="$APP_TEST_CONNECTION_STRING" \
  --database="$APP_TEST_DATABASE"

sleep infinity
