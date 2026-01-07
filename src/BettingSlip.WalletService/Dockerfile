# ─────────────────────────────────────────────
# Base runtime image
# ─────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5011
# ECS/Fargate only needs HTTP port; HTTPS removed for simplicity

# ─────────────────────────────────────────────
# Build stage
# ─────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files first for restore
COPY ["src/BettingSlip.WalletService/BettingSlip.WalletService.csproj", "src/BettingSlip.WalletService/"]
COPY ["src/BettingSlip.Contracts/BettingSlip.Contracts.csproj", "src/BettingSlip.Contracts/"]

# Restore dependencies
RUN dotnet restore "src/BettingSlip.WalletService/BettingSlip.WalletService.csproj"

# Copy remaining source code
COPY . .

WORKDIR "/src/src/BettingSlip.WalletService"

# Build project
RUN dotnet build "BettingSlip.WalletService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ─────────────────────────────────────────────
# Publish stage
# ─────────────────────────────────────────────
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BettingSlip.WalletService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ─────────────────────────────────────────────
# Final runtime image
# ─────────────────────────────────────────────
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ECS entrypoint
ENTRYPOINT ["dotnet", "BettingSlip.WalletService.dll"]
