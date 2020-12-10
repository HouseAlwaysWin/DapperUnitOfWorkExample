# Dapper with Unit Of Work Example

## What is this project for:

This is just a Example for Repository and Unit Of Work pattern with Dapper.
And it's build up with .Net Core 3.1 And .net standard 2.1

## How to use it

Before you start it, you have to install docker otherwise you have to build it up DB and modified connection string with your own self.

just under the root of project and run command:

```
docker-compose --compatibility up --build
```

After the command you will build up the docker image with sqlserver and web api, you can just run the console example in debug mode

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

The web api will run under the http://localhost:5001

if you don't want to run web api with docker you can just stop it from docker and modified launch.json:
```json
   {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      // If you have changed target frameworks, make sure to update the program path.
      "program": "${workspaceFolder}/DapperUnitOfWorkWebApi/bin/Debug/netcoreapp3.1/DapperUnitOfWorkWebApi.dll",
      "args": [],
      "cwd": "${workspaceFolder}/DapperUnitOfWorkWebApi",
      "stopAtEntry": false,
      // Enable launching a web browser when ASP.NET Core starts. For more information: https://aka.ms/VSCode-CS-LaunchJson-WebBrowser
      // "serverReadyAction": {
      //     "action": "openExternally",
      //     "pattern": "^\\s*Now listening on:\\s+(https?://\\S+)"
      // },
      "env": {
          "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "sourceFileMap": {
          "/Views": "${workspaceFolder}/Views"
      }
    }
```
press F5 under the vscode to run by yourslef for testing it

Or you just want to debug under the docker then you can just use attach to check it:
modified launch.json:

```json
 {
   "name": ".NET Core SSH Attach",
   "type": "coreclr",
   "request": "attach",
   "processId": "${command:pickRemoteProcess}",
   "pipeTransport": {
       "pipeProgram": "docker",
       "pipeArgs": [
           "exec",
           "-i",
           "webapi"
       ],
       "debuggerPath": "/root/vsdbg/vsdbg",
       "pipeCwd": "${workspaceRoot}",
       "quoteArgs": false
   },
   // To map current project with docker container, {"container path":"your project path"}
   "sourceFileMap": {
       "/app": "${workspaceRoot}"
   },
  }
```

and attach to dll 









