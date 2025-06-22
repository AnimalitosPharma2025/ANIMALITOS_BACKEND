# Etapa base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el proyecto
COPY . .

# Restaurar paquetes NuGet
RUN dotnet restore "./ANIMALITOS_PHARMA_API.csproj"

# Compilar en Release
RUN dotnet build "./ANIMALITOS_PHARMA_API.csproj" -c Release -o /app/build

# Publicar para producción
RUN dotnet publish "./ANIMALITOS_PHARMA_API.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ANIMALITOS_PHARMA_API.dll"]
