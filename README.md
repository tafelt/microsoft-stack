# Microsoft Stack

## Getting Started

## Known issues
Currently VS Code has timing/ordering issues when installing extensions in devcontainer. Issue has not been resolved yet, so when opening devcontainer you will see an error

`Cannot activate the '<extension>' extension because it depends on the '<other extension>' extension, which is not loaded. Would you like to reload the window to load the extension?`

Clicking "Reload Window" will resolve this issue.

After this you might see

`Configuration file(s) changed: .devcontainer.json. The container might need to be rebuilt to apply the changes.`

which can be ignored.

## How to connect a remote container
- Start VS Code and run `Remote-Containers: Open Folder in Container...` from the Command Palette (F1) and select a folder containing `.devcontainer` folder.
- If you want to connect multiple containers, start up a new window using `File > New Window` and repeat the previous step.

## How to create a new connection to MSSQL server from VS Code
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