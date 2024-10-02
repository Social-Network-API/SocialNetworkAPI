# Usa una imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usa una imagen de SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SocialNetworkApi/SocialNetworkApi.csproj", "SocialNetworkApi/"]
RUN dotnet restore "SocialNetworkApi/SocialNetworkApi.csproj"
COPY . .
WORKDIR "/src/SocialNetworkApi"
RUN dotnet build "SocialNetworkApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialNetworkApi.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialNetworkApi.dll"]
