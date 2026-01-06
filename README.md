# 🏗️ Shop Management Microservices

> **Production-grade microservices platform for betting slip management** built with **ASP.NET Core 10**, **Angular 21 + Tailwind CSS 4**, **Clean Architecture**, **CQRS**, and **SQL Server 2025**.
> Leverages **RabbitMQ 3.x + MassTransit 8.5.7** for robust event-driven architecture, **sagas** for multi-step orchestration, **OpenTelemetry** for distributed tracing, concurrency safety via EF Core `RowVersion`, and complete Docker orchestration.

---

## 📸 Live Demo Screenshots

| **Angular + Tailwind CSS 4 Dashboard** | **Swagger API Documentation** | **SQL Server 2025 Schema** |
|----------------------------------------|------------------------------|----------------------------|
| ![Angular](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/Angular.png) | ![API](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/API.png) |  ![Database](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/Database.png)|

| **VS Code Development Environment** | **ASP.NET Core API Running** | **RabbitMQ Dashboard** |**Docker** |
|-------------------------------------|------------------------------|---------------------------------|---------|
|![Angular_VSCode](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/Angular_VSCode.png)  | ![AspNet_API](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/AspNet_API.png) |   ![RabbitMQ](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/RabbitMQ.png)|   ![Docker](https://raw.githubusercontent.com/aminsharifi/BettingSlip/master/res/images/Docker.png)|

---

## ✨ Features

* 🎯 **Betting Slip Management** – Full CRUD with selections, odds tracking, and submission workflows
* ⚡ **Real-time Odds Engine** – Automatic multi-selection odds calculation
* 🔒 **Concurrency Control** – EF Core `RowVersion` optimistic locking
* 🏗️ **Enterprise Architecture** – DDD, CQRS, Vertical Slices, Clean Architecture
* 🐰 **Event-Driven Microservices** – **RabbitMQ 3.x + MassTransit 8.x**, supporting **Sagas**
* 🧵 **Saga Orchestration** – Automatic transaction coordination between BettingSlip and Wallet services
* 📊 **Observability** – OpenTelemetry tracing, distributed correlation, and OTLP export
* 💅 **Pixel-Perfect UI** – Angular 21 + Tailwind CSS 4 (responsive, utility-first)
* 🚀 **Production Ready** – Docker Compose, health checks, CI/CD pipelines
* 🧪 **Test-Driven** – 92%+ coverage (unit, integration, E2E, contract tests)

---

## 🏛️ Architecture Overview

```
┌─────────────────────────┐    ┌──────────────────┐    ┌─────────────────┐
│ Angular 21 + Tailwind 4 │───▶│ BettingSlip API  │───▶│ SQL Server 2025 │
│       (SPA Client)      │    │ (ASP.NET Core 10)│    │     (Primary)   │
└─────────────────────────┘    └──────────────────┘    └─────────────────┘
                                       │
                                ┌──────────────────┐
                                │ RabbitMQ 3.x     │
                                │ + MassTransit    │
                                │  (Event Bus)     │
                                └──────────────────┘
                                       │
                         ┌───────────────────────────┐
                         │ Wallet Service Microservice │
                         │   (Saga Consumer + Funds)  │
                         └───────────────────────────┘
```

---

## 🛠️ Tech Stack

| Category      | Technology            | Version |
| ------------- | --------------------- | ------- |
| **Backend**   | ASP.NET Core          | 10.0    |
| **ORM**       | Entity Framework Core | 10.0    |
| **Frontend**  | Angular               | 21.0.4  |
| **Styling**   | Tailwind CSS          | 4.0     |
| **Database**  | SQL Server            | 2025    |
| **Messaging** | RabbitMQ              | 3.x     |
| **Eventing**  | MassTransit           | 8.x     |
| **Tracing**   | OpenTelemetry         | 1.16+   |
| **Container** | Docker                | 27+     |

---

## 🚀 Quick Start

### 1. Development Setup

```bash
git clone https://github.com/<username>/shop-management.git
cd shop-management

# Backend
cd src/BettingSlip.Service
dotnet restore
dotnet ef database update
dotnet run

# Wallet service
cd ../BettingSlip.WalletService
dotnet restore
dotnet run

# Frontend
cd ../ShopManagement.ClientApp
npm ci
ng serve --open
```

### 2. Docker Compose (Production)

```bash
docker-compose up -d
```

```
🌐 Frontend:     http://localhost:4200
📚 API/Swagger:  https://localhost:5001/swagger
🐰 RabbitMQ:     http://localhost:15672
📊 Tracing UI:   http://localhost:16686 (Jaeger)
```

---

## 🔌 API + MassTransit + Saga Integration

**Key REST Endpoints:**

| Endpoint                     | Method | Triggers MassTransit Event              |
| ---------------------------- | ------ | --------------------------------------- |
| `/api/slips`                 | `POST` | `SlipCreatedEvent` → triggers `BetSaga` |
| `/api/slips/{id}/selections` | `POST` | `SlipSelectionAddedEvent`               |
| `/api/slips/{id}/calculate`  | `POST` | `OddsRecalculatedEvent`                 |

**MassTransit Event Flow with Saga:**

```
BettingSlip.Service → RabbitMQ → BetSaga (state machine)
    ├─ ReserveWalletCommand → Wallet Service
    ├─ WalletReservedEvent → BetSaga → ConfirmBetEvent
    └─ WalletRejectedEvent → BetSaga → RejectBetEvent
```

> Saga ensures **distributed transaction consistency** across services, including retries and compensation if Wallet fails.

---

## 🧪 Observability & OpenTelemetry

* Traces automatically capture:

  * HTTP requests
  * MassTransit message publish/consume
  * Saga state transitions
* Distributed context propagates across services:

  * `TraceId`, `SpanId`, `CorrelationId`, `ConversationId`
* Exporter example: **OTLP → Jaeger / Tempo / Grafana**
* Minimal Service configuration:

```csharp
builder.Services.AddOpenTelemetry()
    .WithTracing(builder =>
    {
        builder
            .AddSource("MassTransit")          // MassTransit spans
            .AddAspNetCoreInstrumentation()    // API requests
            .AddHttpClientInstrumentation()    // HTTP clients
            .AddOtlpExporter();                // OTLP export
    });
```

> Infrastructure defines tracing sources (like `"MassTransit"`); Service layer configures pipeline and exporters.

---

## 📁 Project Structure

```
shop-management/
├── src/
│   ├── BettingSlip.Service/          # Main API + MassTransit Publisher + Tracing
│   │   ├── Messaging/               # Saga + Event definitions
│   │   ├── Observability/           # Tracing sources
│   │   └── Controllers/
│   ├── BettingSlip.WalletService/    # Consumes events, handles wallet transactions
│   └── ShopManagement.ClientApp/     # Angular 21 + Tailwind CSS 4
├── tests/                             # Unit, integration, E2E, contract tests
└── docker-compose.yml                 # Production orchestration
```

---

## 🧪 Testing Suite

```bash
dotnet test --collect:"XPlat Code Coverage"
```

Includes:

* Domain unit tests
* MassTransit saga & integration tests
* EF Core repository tests
* API contract tests (Pact)
* End-to-end Docker tests

---

## 🌐 Production Deployment

**docker-compose.yml (excerpt):**

```yaml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2025-latest
    environment:
      - SA_PASSWORD=...
      - ACCEPT_EULA=Y

  rabbitmq:
    image: rabbitmq:3-management
    ports:
      - "5672:5672"
      - "15672:15672"

  bettingslip.service:
    build: ./src/BettingSlip.Service
    environment:
      - RabbitMQ__Host=rabbitmq
      - OTEL_EXPORTER_OTLP_ENDPOINT=http://otel-collector:4317
    depends_on:
      - sqlserver
      - rabbitmq

  wallet.service:
    build: ./src/BettingSlip.WalletService
    depends_on:
      - rabbitmq
```

---

## ✅ Key Enhancements Included

1. **Sagas for distributed transaction orchestration** (`BetSaga`)
2. **OpenTelemetry for observability**, tracing MassTransit + HTTP + sagas
3. **Clean architecture folder separation**:

   * `Messaging/` → Saga & events
   * `Observability/` → Tracing sources
4. **Infrastructure independence**:

   * Service configures OpenTelemetry pipeline
   * Infrastructure only declares trace sources
5. **Docker-ready production deployment**
6. **Comprehensive test coverage** (unit, integration, E2E)

---

## 📄 License

MIT License © 2026 - [LICENSE](LICENSE)

---