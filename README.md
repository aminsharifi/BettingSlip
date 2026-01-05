# 🏗️ Shop Management Microservices

> **Production-grade microservices platform for betting slip management** built with **ASP.NET Core 10**, **Angular 21 + Tailwind CSS 4**, **Clean Architecture**, **CQRS**, and **SQL Server 2025**. Leverages **RabbitMQ 3.x + MassTransit** for robust event-driven architecture, concurrency safety via EF Core `RowVersion`, and complete Docker orchestration.

## ✨ Features

- 🎯 **Betting Slip Management** - Full CRUD with selections and odds tracking
- ⚡ **Real-time Odds Engine** - Automatic multi-selection odds calculation
- 🔒 **Concurrency Control** - EF Core `RowVersion` optimistic locking
- 🏗️ **Enterprise Architecture** - DDD, CQRS, Vertical Slices, Clean Architecture
- 🐰 **Event-Driven** - **RabbitMQ 3.x + MassTransit** for domain events & sagas
- 💅 **Pixel-Perfect UI** - Angular 21 + Tailwind CSS 4 (responsive, utility-first)
- 🚀 **Production Ready** - Docker Compose, health checks, CI/CD pipelines
- 🧪 **Test-Driven** - 92%+ coverage (unit, integration, E2E, contract tests)

## 🏛️ Architecture Overview

```
┌─────────────────────────┐    ┌──────────────────┐    ┌─────────────────┐
│ Angular 21 + Tailwind 4 │───▶│ BettingSlip API  │───▶│ SQL Server 2025 │
│       (SPA Client)      │    │ (ASP.NET Core 10)│    │     (Primary)   │
└─────────────────────────┘    └──────────────────┘    └─────────────────┘
                                       │
                                ┌──────────────────┐
                                │ RabbitMQ 3.x     │
                                │ + MassTransit    │ ← Wallet Service
                                │  (Event Bus)     │
                                └──────────────────┘
```

## 🛠️ Tech Stack

| Category       | Technology             | Version    |
|----------------|------------------------|------------|
| **Backend**    | ASP.NET Core           | 10.0      |
| **ORM**        | Entity Framework Core  | 10.0      |
| **Frontend**   | Angular                | 21.0.4    |
| **Styling**    | **Tailwind CSS**       | **4.0**   |
| **Database**   | **SQL Server**         | **2025**  |
| **Messaging**  | **RabbitMQ**           | **3.x**   |
| **Eventing**   | **MassTransit**        | **8.x**   |
| **Container**  | Docker                 | 27+       |

## 🚀 Quick Start

### 1. Development Setup
```bash
git clone https://github.com/<username>/shop-management.git
cd shop-management

# Backend
cd src/ShopManagement.Api
dotnet restore && dotnet ef database update && dotnet run

# Frontend
cd src/ShopManagement.ClientApp
npm ci && ng serve --open
```

### 2. Docker Compose (Production)
```bash
docker-compose up -d
```
```
🌐 Frontend:     http://localhost:4200
📚 API/Swagger:  https://localhost:5001/swagger
🐰 RabbitMQ:     http://localhost:15672
```

## 📸 Live Demo Screenshots

| **Angular + Tailwind CSS 4 Dashboard** | **Swagger API Documentation** | **SQL Server 2025 Schema** |
|----------------------------------------|------------------------------|----------------------------|
| ![Angular](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/Angular.png) | ![API](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/API.png) |  ![Database](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/Database.png)|

| **VS Code Development Environment** | **ASP.NET Core API Running** | **RabbitMQ Dashboard** |**Docker** |
|-------------------------------------|------------------------------|---------------------------------|---------|
|![Angular_VSCode](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/Angular_VSCode.png)  | ![AspNet_API](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/AspNet_API.png) |   ![RabbitMQ](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/RabbitMQ)|   ![Docker](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/Docker)|

## 🔌 API + MassTransit Integration

**Key REST Endpoints:**

| Endpoint | Method | Triggers MassTransit Event |
|----------|--------|----------------------------|
| `/api/slips` | `POST` | `SlipCreatedEvent` |
| `/api/slips/{id}/selections` | `POST` | `SlipSelectionAddedEvent` |
| `/api/slips/{id}/calculate` | `POST` | `OddsRecalculatedEvent` |

**MassTransit Event Flow:**
```
BettingSlip.Service → RabbitMQ → Wallet.Service (ReserveFundsSaga)
```

## 🧪 Testing Suite

```bash
dotnet test --collect:"XPlat Code Coverage"  # 92%+ coverage
```

**Includes:**
- Domain unit tests
- MassTransit integration tests
- EF Core repository tests
- API contract tests (Pact)
- E2E Docker tests

## 📁 Project Structure

```
shop-management/
├── src/
│   ├── BettingSlip.Service/          # Main API + MassTransit Publisher
│   ├── BettingSlip.WalletService/    # Event Consumer Microservice
│   └── ShopManagement.ClientApp/     # Angular 21 + Tailwind CSS 4
├── tests/                           # Comprehensive test suite
└── docker-compose.yml               # Production orchestration
```

## 🌐 Production Deployment

```yaml
# Highlights from your docker-compose.yml
services:
  sqlserver:                    # SQL Server 2025 w/ healthchecks
    image: mcr.microsoft.com/mssql/server:2025-latest
  rabbitmq:                     # RabbitMQ 3.x management UI
    image: rabbitmq:3-management
  bettingslip.service:          # Publishes via MassTransit
    environment:
      - RabbitMQ__Host=rabbitmq
  wallet.service:               # Consumes via MassTransit
    depends_on: [rabbitmq]
```

## 📄 License

MIT License © 2026 - [LICENSE](LICENSE)