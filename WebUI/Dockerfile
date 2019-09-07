FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
COPY /dist /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "WebUI.dll"]