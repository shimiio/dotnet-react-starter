# dotnet-react-starter

<details>
<summary><h2>Extensions & Settings</h2></summary>

### Extensions
C#, C# Dev Kit, .NET Installer

### Install SDK
```
Ctrl + Shift + P

> .NET: Install New .NET SDK
```

### Settings
In the settings in the search: `omnisharp`

Enable: `Dotnet > Server: Use Omnisharp`

</details>

## Stack
- ASP.NET Core 10 — Clean Architecture (Domain, Application, Infrastructure, API)
- React + Vite + TypeScript + Tailwind CSS
- PostgreSQL + EF Core
- Docker Compose
- xUnit + Moq + Testcontainers
- Vitest
- GitHub Actions CI

## Quick Start
### Prerequisites
- Docker Desktop
- .NET 10 SDK
- Node.js 22 + pnpm

### Run
```
docker compose up --build
```

## ASP.NET Core
### 1 - Project Creation
```
mkdir backend
cd backend
dotnet new sln -n MyProject
mkdir src
cd src
dotnet new web -o MyProject.API
cd ..
dotnet sln add src\MyProject.API\MyProject.API.csproj
```

---

### 2 - Tests
#### 2.1 Tests
```
cd backend
mkdir tests
cd tests
dotnet new xunit -n MyProject.UnitTests
cd ..
dotnet sln add tests/MyProject.UnitTests/MyProject.UnitTests.csproj
```
#### 2.2 Install `Moq` & `FluentAssertions`
```
dotnet add tests/MyProject.UnitTests package Moq
dotnet add tests/MyProject.UnitTests package FluentAssertions
```
#### 2.3 Add Reference
```
dotnet add reference ../../src/MyProject
```

---

### 3 - PostgreSQL Database
#### 3.1 Packages in `MyProject.Infrastructure`:
```
dotnet add src/MyProject.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add src/MyProject.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add src/MyProject.Infrastructure package Microsoft.EntityFrameworkCore.Design
```
#### 3.2 Packages in `MyProject.API`:
```
dotnet add src/MyProject.API package Microsoft.EntityFrameworkCore.Design
```
#### 3.3 Set Connection String:
In `appsettings.json`:
```
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=myproject;Username=postgres;Password=postgres"
  },
```
#### 3.4 EF Core CLI
```
dotnet tool install --global dotnet-ef
dotnet ef --version
```
---

### Packages
```
swagger:
dotnet add src/MyProject.API package Swashbuckle.AspNetCore

integration:
dotnet add tests/MyProject.IntegrationTests package Testcontainers.PostgreSql
dotnet add tests/MyProject.IntegrationTests package Microsoft.AspNetCore.Mvc.Testing
dotnet add tests/MyProject.IntegrationTests package FluentAssertions
```

---

### Launch
```
launches the project:
dotnet run

builds a project:
dotnet build

testing the project:
dotnet test
```

## React
### 1 - React Template
https://github.com/shimiio/react-template

---

### 2 - Tests Packages
```
pnpm add -D @vitest/coverage-v8 @testing-library/react @testing-library/jest-dom jsdom
```
