# Microsoft Stack

This project presents the use of Microsoft technologies in VS Code `devcontainer` development environments.

## Prerequisites
- For Windows users only
  - Install Ubuntu 22.04 LTS on WSL
  - Set Ubuntu to use WSL 2
    - `wsl --set-version Ubuntu 22.04 2`
- Install VS Code
- Install Docker Desktop
- Set-up VS Code for remote development by installing extension called "Remote Development" `ms-vscode-remote.vscode-remote-extensionpack`
- Configure Docker (and WSL integration)
  - For Windows users only
    - Settings > General > Use the WSL 2 based engine
    - Settings > Resources > WSL integration > Enable integration with my default WSL distro
  - For MacOS users only
    - Settings > General > Use Virtualization framework > VirtioFS
    - Settings > General > Use Rosetta for x86/AMD64 emulation on Apple Silicon
- Read the FAQ section if you're not familiar with connecting a remote container via VS Code
- Read the known issues section

## Getting Started

- Clone the project to the desired location
  - For better performance, Windows users should clone the repo under WSL `\\wsl$\Ubuntu-22.04\home\<username>\<folder>\`
- Create an `.env` file at the root of the project based on the `.env.example` file
  - If you are using a MacOS remember to change the default build platform for Apple Silicon
- To develop the different solutions, all you need to do is open the `./Client`, `./Database` or `./Server/` folders on the remote container
  - Wait for container to install all dependencies and extensions
  - After this, you can use the solution normally

## Known issues

### VS Code

Currently VS Code has timing/ordering issues when installing extensions in devcontainer. Issue has not been resolved yet, so when opening devcontainer you will see an error

`Cannot activate the '<extension>' extension because it depends on the '<other extension>' extension, which is not loaded. Would you like to reload the window to load the extension?`

Clicking "Reload Window" will resolve this issue.

After this you might see

`Configuration file(s) changed: .devcontainer.json. The container might need to be rebuilt to apply the changes.`

which can be ignored.

## FAQ

### How to connect a remote container
- Start VS Code and run `Remote-Containers: Open Folder in Container...` from the Command Palette (F1) and select a folder containing `.devcontainer` folder.
- If you want to connect multiple containers, start up a new window using `File > New Window` and repeat the previous step.

### How to create a new connection to MSSQL server from VS Code
- Go to the `SQL Server` tab in the VS Code editor
- Click `Add Connection` and fill in the following fields as follows
  - Server name or ADO connection string
    - Insert `mssql` (which is docker-compose service name)
  - Database name (optional)
    - Skip by pressing enter
  - Authentication Type
    - Choose `SQL Login`
  - User name
    - Insert `sa`
  - Password
    - Insert your `MSSQL_SA_PASSWORD` password