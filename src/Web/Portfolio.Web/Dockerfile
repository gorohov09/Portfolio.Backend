#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/Web/Portfolio.Web/Portfolio.Web.csproj", "src/Web/Portfolio.Web/"]
COPY ["src/Core/Portfolio.Core/Portfolio.Core.csproj", "src/Core/Portfolio.Core/"]
COPY ["src/Contracts/Portfolio.Contracts/Portfolio.Contracts.csproj", "src/Contracts/Portfolio.Contracts/"]
COPY ["src/Core/Portfolio.Domain/Portfolio.Domain.csproj", "src/Core/Portfolio.Domain/"]
COPY ["src/Infrastructure/Portfolio.Data.PostgreSql/Portfolio.Data.PostgreSql.csproj", "src/Infrastructure/Portfolio.Data.PostgreSql/"]
COPY ["src/Infrastructure/Portfolio.Data.S3/Portfolio.Data.S3.csproj", "src/Infrastructure/Portfolio.Data.S3/"]
RUN dotnet restore "./src/Web/Portfolio.Web/./Portfolio.Web.csproj"
COPY . .
WORKDIR "/src/src/Web/Portfolio.Web"
RUN dotnet build "./Portfolio.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Portfolio.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Web.dll"]