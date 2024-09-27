# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore dependencies
COPY ["SocialNetworkApi/SocialNetworkApi.csproj", "SocialNetworkApi/"]
RUN dotnet restore "SocialNetworkApi/SocialNetworkApi.csproj"

# Copy everything else and build the application
COPY ["SocialNetworkApi/", "SocialNetworkApi/"]
WORKDIR /src/SocialNetworkApi
RUN dotnet build "SocialNetworkApi.csproj" -c Release -o /app/build

# Stage 2: Publish Stage
FROM build AS publish
RUN dotnet publish "SocialNetworkApi.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialNetworkApi.dll"]
