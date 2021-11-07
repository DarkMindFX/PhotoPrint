FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY bin/Release/net5.0 PhotoPrint.API/
WORKDIR /PhotoPrint.API
ENV ASPNETCORE_ENVIRONMENT=Prod
ENV ASPNETCORE_URLS=http://+:8082
ENTRYPOINT ["dotnet", "PhotoPrint.API.dll"]