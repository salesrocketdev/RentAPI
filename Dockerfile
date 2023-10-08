# Usar a imagem base ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Usar a imagem base do SDK do .NET Core para construção
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rent.API/Rent.API.csproj", "Rent.API/"]
COPY ["Rent.Core/Rent.Core.csproj", "Rent.Core/"]
COPY ["Rent.Domain/Rent.Domain.csproj", "Rent.Domain/"]
COPY ["Rent.Infrastructure/Rent.Infrastructure.csproj", "Rent.Infrastructure/"]
RUN dotnet restore "Rent.API/Rent.API.csproj"
COPY . .
WORKDIR "/src/Rent.API"
RUN dotnet build "Rent.API.csproj" -c Release -o /app/build

# Publicar o aplicativo
FROM build AS publish
RUN dotnet publish "Rent.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio final para copiar os artefatos e definir o ponto de entrada
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rent.API.dll"]
