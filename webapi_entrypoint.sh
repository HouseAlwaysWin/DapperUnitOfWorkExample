#!/bin/bash


# urls domain must be 0.0.0.0, to expose ports to outside
run_cmd="dotnet watch -p DapperUnitOfWorkWebApi run"
exec $run_cmd