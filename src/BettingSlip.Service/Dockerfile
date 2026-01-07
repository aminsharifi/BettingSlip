# ────────────────────────────────────────────────
# Base image for runtime
# ────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5001
# Only expose 5001, as ECS container uses single port
# ECS automatically maps host port

# ────────────────────────────────────────────────
# Build image
# ────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files first for restore
COPY ["src/BettingSlip.Service/BettingSlip.Service.csproj", "src/BettingSlip.Service/"]
COPY ["src/BettingSlip.Infrastructure/BettingSlip.Infrastructure.csproj", "src/BettingSlip.Infrastructure/"]
COPY ["src/BettingSlip.Application/BettingSlip.Application.csproj", "src/BettingSlip.Application/"]
COPY ["src/BettingSlip.Core/BettingSlip.Core.csproj", "src/BettingSlip.Core/"]
COPY ["src/BettingSlip.Contracts/BettingSlip.Contracts.csproj", "src/BettingSlip.Contracts/"]

RUN dotnet restore "src/BettingSlip.Service/BettingSlip.Service.csproj"

# Copy the rest of the code
COPY . .

WORKDIR "/src/src/BettingSlip.Service"

# Build the project
RUN dotnet build "BettingSlip.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ────────────────────────────────────────────────
# Publish the project
# ────────────────────────────────────────────────
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BettingSlip.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ────────────────────────────────────────────────
# Final stage for ECS/Fargate
# ────────────────────────────────────────────────
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Use only one port (5001) to match ECS Task Definition
ENTRYPOINT ["dotnet", "BettingSlip.Service.dll"]
