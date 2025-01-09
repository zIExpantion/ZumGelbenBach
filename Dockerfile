# 1. Build-Phase
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Kopiere die .csproj-Datei und stelle Abhängigkeiten wieder her
COPY ZumGelbenBach/*.csproj ./
RUN dotnet restore

# Kopiere den Rest des Codes und baue das Projekt
COPY ZumGelbenBach/ ./
RUN dotnet publish -c Release -o /publish

# 2. Laufzeit-Phase (für WebAssembly)
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Kopiere die veröffentlichten Blazor WebAssembly-Dateien ins NGINX-Verzeichnis
COPY --from=build /publish/wwwroot .

# Kopiere die NGINX-Konfigurationsdatei
COPY nginx.conf /etc/nginx/nginx.conf

# Port 80 für den Webserver freigeben
EXPOSE 80

# NGINX im Vordergrund ausführen
CMD ["nginx", "-g", "daemon off;"]
