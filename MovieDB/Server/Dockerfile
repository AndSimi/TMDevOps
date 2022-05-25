FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
COPY Server/bin/Release/net6.0 App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Server.dll"]