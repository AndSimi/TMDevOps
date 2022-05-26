FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY Server/bin/Release/net6.0/ app/
WORKDIR /app
ENTRYPOINT ["dotnet", "/app/publish/Server.dll"]