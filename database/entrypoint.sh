#!/bin/bash -e

export DATA_SOURCE="mssql"
export DATABASE_APP="appdb"
export DATABASE_APP_TEST="${DATABASE_APP}_test"
export USER_ID="sa"
export PASSWORD="<YourStrong!Passw0rd>"

export MASTER_CONNECTION_STRING="Data Source=${DATA_SOURCE};User ID=${USER_ID};Password=${PASSWORD};"
export APPLICATION_CONNECTION_STRING="Data Source=${DATA_SOURCE};Database=${DATABASE_APP};User ID=${USER_ID};Password=${PASSWORD};"
export APPLICATION_TEST_CONNECTION_STRING="Data Source=${DATA_SOURCE};Database=${DATABASE_APP_TEST};User ID=${USER_ID};Password=${PASSWORD};"

dotnet restore

dotnet clean /usr/src/database/database.sln --configuration Release

dotnet build /usr/src/database/database.sln --configuration Release --no-incremental

dotnet run \
  --configuration Release \
  --project=Migrator \
  --masterConnectionString="$MASTER_CONNECTION_STRING" \
  --applicationConnectionString="$APPLICATION_CONNECTION_STRING" \
  --applicationDatabase="$DATABASE_APP"

dotnet run \
  --configuration Release \
  --project=Migrator \
  --masterConnectionString="$MASTER_CONNECTION_STRING" \
  --applicationConnectionString="$APPLICATION_TEST_CONNECTION_STRING" \
  --applicationDatabase="$DATABASE_APP_TEST"
