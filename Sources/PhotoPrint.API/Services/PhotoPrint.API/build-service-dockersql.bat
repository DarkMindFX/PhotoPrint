xcopy ".\appsettings.DockerSql.json" "bin\Release\net5.0\appsettings.json" /Y

docker build -t globus000/ppt.photoprint.api-test-dockersql -f "Dockerfile" .