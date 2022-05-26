FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY MovieDB\Server\bin\Release\net6.0 app/publish
ENTRYPOINT ["dotnet", "/app/publish/Server.dll"]