# 📦 Etapa 1: Construcción (Build)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos solo el .csproj primero para aprovechar el caché de Docker
COPY ["backend.csproj", "./"]
RUN dotnet restore "backend.csproj"

# Copiamos el resto del código fuente y compilamos la aplicación
COPY . .
RUN dotnet publish "backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 🚀 Etapa 2: Ejecución (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Copiamos los archivos compilados de la Etapa 1
COPY --from=build /app/publish .

# Comando para iniciar la aplicación
ENTRYPOINT ["dotnet", "backend.dll"]