FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
COPY dist /app
WORKDIR /app
EXPOSE 80/tcp
# ENV WAITHOST=mysql WAITPORT=3306
ENTRYPOINT exec dotnet WebUI.dll