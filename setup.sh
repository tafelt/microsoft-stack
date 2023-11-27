#!/bin/bash

# General
read -p "Name the project (default: microsoft-stack) " PROJECT
PROJECT=${PROJECT:-"microsoft-stack"}

read -p "Are you using an Apple Silicon processor Y/N (skipping equals N)? " IS_USING_APPLE_SILICON
if [[ $IS_USING_APPLE_SILICON == 'Y' ]]
then
  BUILD_PLATFORM="linux/arm64/v8"
else
  BUILD_PLATFORM="linux/amd64"
fi

# Database
MSSQL_VARIANT="2022-latest"
MSSQL_ACCEPT_EULA="Y"
MSSQL_PID="Developer"
MSSQL_USER_ID="sa"
MSSQL_DATA_SOURCE="mssql"
MSSQL_TCP_PORT="1433"

read -p "Set the MSSQL SA password (default: <YourStrong!Passw0rd>) " MSSQL_SA_PASSWORD
MSSQL_SA_PASSWORD=${MSSQL_SA_PASSWORD:-"<YourStrong!Passw0rd>"}

read -p "Name the application database (default: appdb) " MSSQL_APP_DATABASE
MSSQL_APP_DATABASE=${MSSQL_APP_DATABASE:-"appdb"}

# Server
DOTNET_VARIANT="7.0"
DOTNET_APP_CONNECTION_STRING="Data Source=${MSSQL_DATA_SOURCE};Database=${MSSQL_APP_DATABASE};User ID=${MSSQL_USER_ID};Password=${MSSQL_SA_PASSWORD};Trusted_Connection=false;TrustServerCertificate=true"

read -p "Set the server port number (default: 5000) " DOTNET_PORT
DOTNET_PORT=${DOTNET_PORT:-"5000"}
DOTNET_URLS="http://0.0.0.0:${DOTNET_PORT}"

# Client
read -p "Set the client port number (default: 3000) " REACT_PORT
REACT_PORT=${REACT_PORT:-"3000"}

cat <<EOF >.env
# General
PROJECT = ${PROJECT}
BUILD_PLATFORM = ${BUILD_PLATFORM}

# Database
MSSQL_VARIANT = ${MSSQL_VARIANT}
MSSQL_ACCEPT_EULA = ${MSSQL_ACCEPT_EULA}
MSSQL_PID = ${MSSQL_PID}
MSSQL_USER_ID = ${MSSQL_USER_ID}
MSSQL_SA_PASSWORD = ${MSSQL_SA_PASSWORD}
MSSQL_DATA_SOURCE = ${MSSQL_DATA_SOURCE}
MSSQL_TCP_PORT = ${MSSQL_TCP_PORT}
MSSQL_APP_DATABASE = ${MSSQL_APP_DATABASE}

# Server
DOTNET_VARIANT = ${DOTNET_VARIANT}
DOTNET_PORT = ${DOTNET_PORT}
DOTNET_URLS = "${DOTNET_URLS}"
DOTNET_APP_CONNECTION_STRING = "${DOTNET_APP_CONNECTION_STRING}"

# Client
REACT_PORT = ${REACT_PORT}
EOF
