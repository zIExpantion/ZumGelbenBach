# Schritt 1: Build-Phase (mit .NET SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Kopiere Projektdateien und stelle Abhängigkeiten her
COPY *.csproj ./
RUN dotnet restore

# Kopiere den Code und baue die Anwendung
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Schritt 2: Bereitstellungs-Phase (mit Nginx als Webserver)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Kopiere die veröffentlichten Dateien aus der Build-Phase in das Verzeichnis /app
COPY --from=build /app/publish .

# Exponiere den Port 80 für die Webanwendung
EXPOSE 80

# Starte die ASP.NET Core-Anwendung
CMD ["dotnet", "ZumGelbenBach.dll"]
