# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Backend/UserTransactions.API/UserTransactions.API.csproj", "src/Backend/UserTransactions.API/"]
COPY ["src/Shared/UserTransactions.Communication/UserTransactions.Communication.csproj", "src/Shared/UserTransactions.Communication/"]
COPY ["src/Backend/UserTransactions.Domain/UserTransactions.Domain.csproj", "src/Backend/UserTransactions.Domain/"]
COPY ["src/Shared/UserTransactions.Exception/UserTransactions.Exception.csproj", "src/Shared/UserTransactions.Exception/"]
COPY ["src/Backend/UserTransactions.Application/UserTransactions.Application.csproj", "src/Backend/UserTransactions.Application/"]
COPY ["src/Backend/UserTransactions.Infrastructure/UserTransactions.Infrastructure.csproj", "src/Backend/UserTransactions.Infrastructure/"]
RUN dotnet restore "./src/Backend/UserTransactions.API/UserTransactions.API.csproj"
COPY . .
WORKDIR "/src/src/Backend/UserTransactions.API"
RUN dotnet build "./UserTransactions.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserTransactions.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserTransactions.API.dll"]