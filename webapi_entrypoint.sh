#!/bin/bash


# urls domain must be 0.0.0.0, to expose ports to outside
run_cmd="dotnet watch -p DapperUnitOfWorkWebApi run --urls=http://0.0.0.0:5001/"
exec $run_cmd