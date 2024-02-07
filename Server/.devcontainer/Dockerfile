FROM mcr.microsoft.com/devcontainers/dotnet:7.0

WORKDIR /usr/src/Server

COPY --chown=vscode:vscode . .

ENTRYPOINT ["./entrypoint.sh"]
CMD ["sleep", "infinity"]
