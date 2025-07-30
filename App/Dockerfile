# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["App.csproj", "App/"]
COPY . .
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ../*.sln ./
COPY App/*.csproj ./App/

RUN dotnet restore

# Copia o restante do código da aplicação
COPY App/. ./App/

WORKDIR /src/App
RUN dotnet publish -c Release -o /app/publish

# --- Etapa de Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "App.dll"]