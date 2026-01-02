# 🏗 Shop Management Microservices

[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-21.0.4-red)](https://angular.io/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

> **Full-stack microservices project for managing betting slips and selections**, built with **ASP.NET Core 10**, **Angular 21**, and **SQL Server**. Follows **Clean Architecture** and **CQRS** principles.

---

## 📖 Table of Contents

* [Project Overview](#project-overview)
* [Architecture](#architecture)
* [Technologies](#technologies)
* [Setup & Run](#setup--run)
* [API Documentation](#api-documentation)
* [Testing](#testing)
* [Project Structure](#project-structure)
* [Contributing](#contributing)
* [License](#license)

---

## 🏗 Project Overview

This project is a **microservices-based system for managing betting slips and selections**:

* Create and manage betting slips
* Add selections with odds
* Calculate total odds for a slip
* Ensure concurrency safety using EF Core `RowVersion`
* Follows **Domain-Driven Design (DDD)** and **Clean Architecture** principles

---

## 🏛 Architecture

**Layers & Responsibilities:**

| Layer              | Technology          | Responsibility                                    |
| ------------------ | ------------------- | ------------------------------------------------- |
| **Domain**         | C#                  | Business rules, aggregates, value objects, events |
| **Application**    | C#                  | Use cases, commands/handlers, interfaces          |
| **Infrastructure** | EF Core, SQL Server | Database, repositories, persistence               |
| **API**            | ASP.NET Core 10     | REST endpoints, controllers, DTOs                 |
| **Frontend**       | Angular 21          | Client-side SPA, forms, API calls                 |

**Key Principles:**

* CQRS (Command/Query Responsibility Segregation)
* Microservices-ready design
* Concurrency-safe operations (`RowVersion`)
* DTO-based API contracts
* Clean separation of concerns

---

## 🛠 Technologies

* **Backend:** ASP.NET Core 10, C#, EF Core 10
* **Frontend:** Angular 21, SCSS, Angular Router
* **Database:** SQL Server
* **Testing:** xUnit, FluentAssertions, HttpClient (E2E tests)
* **Tools:** Visual Studio 2026, VS Code, Git

---

## ⚡ Setup & Run

### 1️⃣ Clone the repository

```bash
git clone https://github.com/<username>/shop-management.git
cd shop-management
```

### 2️⃣ Run Backend

Ensure **SQL Server** is running and the connection string is set in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ShopDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Apply migrations:

```bash
cd src/ShopManagement.Api
dotnet ef database update
dotnet run
```

API runs at:

```
https://localhost:5001
```

---

### 3️⃣ Run Frontend

```bash
cd src/ShopManagement.ClientApp
npm install
ng serve --open
```

Angular app runs at:

```
http://localhost:4200
```

---

## 📌 API Documentation

Swagger is available at:

```
https://localhost:5001/swagger
```

**Example endpoints:**

| Method | URL                     | Description                             |
| ------ | ----------------------- | --------------------------------------- |
| POST   | `/api/slips`            | Create a betting slip                   |
| POST   | `/api/slips/selections` | Add a selection to a slip               |
| GET    | `/api/slips/{id}`       | Retrieve a betting slip with selections |

**Sample response for creating a slip:**

```json
{
  "id": "546825c4-d763-40b2-8d8d-37d0b99dc0ea"
}
```

---

## 🧪 Testing

### Domain & Application Tests

```bash
cd tests/ShopManagement.Domain.Tests
dotnet test

cd tests/ShopManagement.Application.Tests
dotnet test
```

### E2E API Tests

```bash
cd tests/ShopManagement.E2E.Tests
dotnet test
```

> E2E tests use **real HTTP calls** and **SQL Server** to simulate a production environment.

---

## 📂 Project Structure

```
src/
 ├── ShopManagement.Domain
 ├── ShopManagement.Application
 ├── ShopManagement.Infrastructure
 ├── ShopManagement.Api
 └── ShopManagement.ClientApp
tests/
 ├── ShopManagement.Domain.Tests
 ├── ShopManagement.Application.Tests
 └── ShopManagement.E2E.Tests
```

---

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch: `git checkout -b feature/my-feature`
3. Commit your changes: `git commit -m 'Add new feature'`
4. Push to branch: `git push origin feature/my-feature`
5. Open a pull request

---

## 📄 License

MIT License © 2026

---