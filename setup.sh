#!/bin/bash

# General
read -p "Name the project (default: microsoft-stack) " PROJECT
PROJECT=${PROJECT:-"microsoft-stack"}

read -p "Are you using an Apple Silicon processor (y/n)? | skipping equals (n) " IS_USING_APPLE_SILICON
if [[ ${IS_USING_APPLE_SILICON,,} == 'y' ]]
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
MSSQL_APP_TEST_DATABASE="${MSSQL_APP_DATABASE}_test"

MSSQL_SYSTEM_CONNECTION_STRING="Data Source=${MSSQL_DATA_SOURCE};User ID=${MSSQL_USER_ID};Password=${MSSQL_SA_PASSWORD};"
MSSQL_APP_CONNECTION_STRING="Data Source=${MSSQL_DATA_SOURCE};Database=${MSSQL_APP_DATABASE};User ID=${MSSQL_USER_ID};Password=${MSSQL_SA_PASSWORD};Trusted_Connection=false;TrustServerCertificate=true;"
MSSQL_APP_TEST_CONNECTION_STRING="Data Source=${MSSQL_DATA_SOURCE};Database=${MSSQL_APP_TEST_DATABASE};User ID=${MSSQL_USER_ID};Password=${MSSQL_SA_PASSWORD};Trusted_Connection=false;TrustServerCertificate=true;"

# Server
DOTNET_VARIANT="7.0"

read -p "Set the server port number (default: 5000) " DOTNET_PORT
export DOTNET_PORT=${DOTNET_PORT:-"5000"}
DOTNET_URLS="http://0.0.0.0:${DOTNET_PORT}"

# Client
read -p "Set the client port number (default: 3000) " REACT_PORT
export REACT_PORT=${REACT_PORT:-"3000"}

# Nginx
NGINX_VARIANT="1.25"

read -p "Set the reverse proxy port number (default: 8080) " NGINX_PORT
export NGINX_PORT=${NGINX_PORT:-"8080"}

# Create .env file
cat <<EOF >.env
# General
PROJECT=${PROJECT}
BUILD_PLATFORM=${BUILD_PLATFORM}

# Database
MSSQL_VARIANT=${MSSQL_VARIANT}
MSSQL_ACCEPT_EULA=${MSSQL_ACCEPT_EULA}
MSSQL_PID=${MSSQL_PID}
MSSQL_USER_ID=${MSSQL_USER_ID}
MSSQL_SA_PASSWORD=${MSSQL_SA_PASSWORD}
MSSQL_DATA_SOURCE=${MSSQL_DATA_SOURCE}
MSSQL_TCP_PORT=${MSSQL_TCP_PORT}
MSSQL_APP_DATABASE=${MSSQL_APP_DATABASE}
MSSQL_APP_TEST_DATABASE=${MSSQL_APP_TEST_DATABASE}
MSSQL_SYSTEM_CONNECTION_STRING="${MSSQL_SYSTEM_CONNECTION_STRING}"
MSSQL_APP_CONNECTION_STRING="${MSSQL_APP_CONNECTION_STRING}"
MSSQL_APP_TEST_CONNECTION_STRING="${MSSQL_APP_TEST_CONNECTION_STRING}"

# Server
DOTNET_VARIANT=${DOTNET_VARIANT}
DOTNET_PORT=${DOTNET_PORT}
DOTNET_URLS="${DOTNET_URLS}"

# Client
REACT_PORT=${REACT_PORT}

# Nginx
NGINX_VARIANT=${NGINX_VARIANT}
NGINX_PORT=${NGINX_PORT}
EOF

# Create nginx.conf file
envsubst "\$DOTNET_PORT \$REACT_PORT \$NGINX_PORT" < ./Nginx/nginx.conf.template > ./Nginx/nginx.conf
