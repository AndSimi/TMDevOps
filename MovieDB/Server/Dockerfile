FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY ./bin/Release/net6.0/ /app/publish
ENTRYPOINT ["dotnet", MovieDB.Server.dll]