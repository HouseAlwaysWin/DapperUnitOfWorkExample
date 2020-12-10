# DapperUnitOfWorkExample

## What is this project for:

This is just a Example for Repository and Unit Of Work pattern with Dapper

## How to use it

just under the root of project and run command:

```
docker-compose --compatibility up --build
```

After command you will build up the docker image with sqlserver and web api, you can just run the console example in debug mode

just to remove comment from .vscode/launch.json 

```json
   {
         "name": ".NET Core Launch (console)",
         "type": "coreclr",
         "request": "launch",
         "preLaunchTask": "build"
         "program": "${workspaceFolder}/DapperUnitOfWorkConsole/bin/Debug/netcoreapp3.1/DapperUnitOfWorkConsole.dll",
         "args": [],
         "cwd": "${workspaceFolder}/DapperUnitOfWorkConsole"
         "console": "internalConsole",
         "stopAtEntry": false
    },
```

or use postman to test the web api is working


