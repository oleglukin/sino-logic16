FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
COPY /dist /app
WORKDIR /app
EXPOSE 5004/tcp
ENTRYPOINT ["dotnet", "JobTypeA.dll"]