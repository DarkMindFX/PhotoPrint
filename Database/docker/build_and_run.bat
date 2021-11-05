docker build -t globus000/mssqlserver .

docker run --name docker_mssqlserver -it -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -e 'MSSQL_PID=Express' -p 1433:1433 globus000/mssqlserver 

docker attach docker_mssqlserver