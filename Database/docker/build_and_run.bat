docker rm ppt.photoprint.sqldb

.\build.bat

docker run --name ppt.photoprint.sqldb -it -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=PhotoPrint2021!SaAccount' -e 'MSSQL_PID=Express' -p 2433:1433 globus000/ppt.photoprint.sqldb