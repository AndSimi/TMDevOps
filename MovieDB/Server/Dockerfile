FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY Server/bin/Release/net6.0/ /App/
ENTRYPOINT ["dotnet", MovieDB.Server.dll]