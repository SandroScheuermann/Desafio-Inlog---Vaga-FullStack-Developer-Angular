# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie os arquivos de projeto e restaure as dependências
COPY Inlog.Desafio.Backend/Inlog.Desafio.Backend.WebApi/*.csproj ./Inlog.Desafio.Backend/Inlog.Desafio.Backend.WebApi/
RUN dotnet restore ./Inlog.Desafio.Backend/Inlog.Desafio.Backend.WebApi/Inlog.Desafio.Backend.WebApi.csproj

# Copie o restante do código fonte
COPY Inlog.Desafio.Backend/. ./Inlog.Desafio.Backend/

# Publique a aplicação
WORKDIR /app/Inlog.Desafio.Backend
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Inlog.Desafio.Backend/out ./

# Exponha a porta 5000
EXPOSE 5000

# Defina a variável de ambiente para especificar a porta
ENV ASPNETCORE_URLS=http://+:5000

# Inicie a aplicação
ENTRYPOINT ["dotnet", "Inlog.Desafio.Backend.WebApi.dll"]

