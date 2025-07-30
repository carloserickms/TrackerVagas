# Dockerfile (em App/Dockerfile)

# Etapa de Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia o arquivo .sln da raiz do repositório para /src no contêiner
# O Render está no contexto da raiz do repositório, então ".." é a raiz do repo.
# Se seu Dockerfile ESTÁ na pasta App/
COPY ../*.sln ./

# Copia o arquivo .csproj do diretório App (que é o diretório atual do Dockerfile)
# para a pasta /src/App/ no contêiner.
COPY App.csproj ./App/ # Assumindo App.csproj está no mesmo diretório que o Dockerfile

# Restaura as dependências do projeto
RUN dotnet restore ./App/App.csproj # Restaura para o projeto específico

# Copia o restante dos arquivos do projeto (da pasta App/ onde o Dockerfile está)
COPY . ./App/ # Copia TUDO do contexto atual (App/) para /src/App/

WORKDIR /src/App # Muda o diretório de trabalho para dentro da pasta do projeto
RUN dotnet publish -c Release -o /app/publish

# Etapa de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "App.dll"] # Substitua 'App.dll' pelo nome real do seu projeto compilado