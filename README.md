# SocialMedia-App

A scalable social media backend API built with ASP.NET Core, following Clean Architecture and CQRS principles. Focused on real-time communication, performance, and clean code.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=c-sharp)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoft-sql-server)
![Redis](https://img.shields.io/badge/Redis-DC382D?style=flat-square&logo=redis)
![SignalR](https://img.shields.io/badge/SignalR-512BD4?style=flat-square&logo=dotnet)

---

## Overview

This project is a full-featured social media backend covering user authentication, content management (posts, stories, comments), real-time chat and notifications via SignalR, Redis caching with automatic invalidation, and background job processing with Hangfire.

---

## Project Structure

```
SocialMedia-App/
├── Core/               # Entities, interfaces, domain logic
├── Data/               # EF Core DbContext, migrations, configurations
├── Infrastructure/     # Repositories, Unit of Work, external services
├── Services/           # CQRS handlers (MediatR), business logic
└── SocialMedia-App/    # API controllers, middleware, dependency injection
```

---

## Features

**Authentication**
- ASP.NET Identity + JWT stored in HttpOnly Cookies
- Refresh token rotation
- OTP verification and password reset via Redis + SMTP email
- Role-based access control (RBAC)

**Social**
- Posts, comments, reactions, and like system
- Stories with auto-expiration after 24h (Hangfire background job)
- Follow / unfollow with follow request accept & cancel
- Follow-based feed with chronological sorting and pagination

**Real-Time (SignalR)**
- Private chat
- Live notifications for likes, comments, and follow requests
- Unread count tracking and mark-as-read

**Performance**
- Redis cache with automatic invalidation on any write operation (likes, comments, etc.)
- Pagination across all list endpoints

**Media**
- Image and video upload via a dedicated FileService

---

## Tech Stack

| | |
|---|---|
| Framework | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Cache | Redis |
| Real-Time | SignalR |
| CQRS | MediatR |
| Background Jobs | Hangfire |
| Mapping | AutoMapper |
| Validation | FluentValidation |

**Patterns:** Clean Architecture · CQRS · Repository Pattern · Unit of Work · Specification Pattern

---

## Getting Started

**Prerequisites:** .NET 8 SDK, SQL Server, Redis

```bash
git clone https://github.com/AhmedKhaled54/SocialMedia-App.git
cd SocialMedia-App
```

Configure `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your_sql_server_connection_string"
  },
  "JWT": {
    "Key": "your_secret_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience",
    "DurationInDays": 1
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  },
  "MailSettings": {
    "Email": "your_email",
    "Password": "your_password",
    "Host": "smtp.gmail.com",
    "Port": 587
  }
}
```

Apply migrations and run:

```bash
dotnet ef database update --project Data --startup-project SocialMedia-App
dotnet run --project SocialMedia-App
```

API docs available at `/swagger` after running.

---

## Author

Ahmed Khaled — Backend .NET Developer

[LinkedIn](https://www.linkedin.com/in/ahmed-khaled-b32676348/) · [GitHub](https://github.com/AhmedKhaled54) · [Email](ahmed.khaled0132@gmail.com)
