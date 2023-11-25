FROM mcr.microsoft.com/devcontainers/dotnet:0-7.0

WORKDIR /usr/src/Server

COPY --chown=vscode:vscode . .

ENTRYPOINT ["./entrypoint.sh"]
