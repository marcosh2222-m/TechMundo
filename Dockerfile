# Imagen de TechMundo para publicar en Render (o cualquier servicio que ejecute contenedores Docker).
# Etapa 1: compilar y publicar con el SDK de .NET 8.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY TechMundo.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# Etapa 2: imagen liviana solo con el runtime de ASP.NET Core.
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Render envía el tráfico al puerto 10000 por defecto.
# La base SQLite vive dentro del contenedor: en el plan gratis se reinicia con los datos de ejemplo
# cada vez que el servicio se duerme o se vuelve a publicar (ideal para una demo que arranca limpia).
ENV ASPNETCORE_ENVIRONMENT=Production \
    ASPNETCORE_HTTP_PORTS=10000 \
    ConnectionStrings__Default="Data Source=/app/data/techmundo.db"
EXPOSE 10000

ENTRYPOINT ["dotnet", "TechMundo.dll"]
