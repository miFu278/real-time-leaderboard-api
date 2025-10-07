# 🏆 Real-Time Leaderboard System

A **real-time leaderboard service** built with **ASP.NET Core 8**, **PostgreSQL**, and **Redis** — designed using **Clean Architecture** and **CQRS** principles.
It enables users to compete in various games, track their scores, and view live rankings on dynamic leaderboards.

---

## 🚀 Features

### 🧩 Core

* Clean Architecture with Domain, Application, Infrastructure, and API layers
* CQRS pattern with MediatR for commands and queries
* Real-time updates via SignalR
* API documentation with Swagger/OpenAPI

### 🔒 Authentication & Authorization

* JWT-based authentication
* Secure password hashing with BCrypt
* Role-based access control (User/Admin)

### 🕹️ Game & Score System

* Score submission and validation
* Historical score tracking
* User rank calculation
* Top players reporting
* Date-range filtering for leaderboards

### ⚡ Real-Time Features

* Redis Sorted Sets for instant leaderboard updates
* SignalR for real-time push notifications
* Live rank and leaderboard refresh events

### 🧠 Data & Caching

* PostgreSQL + EF Core for persistent storage
* Redis for caching and leaderboard data
* Efficient leaderboard queries with sorted sets

---

## 🧰 Tech Stack

| Component               | Technology                     |
| ----------------------- | ------------------------------ |
| **Framework**           | ASP.NET Core 8                 |
| **Language**            | C# 12                          |
| **Database**            | PostgreSQL 15+ (EF Core 8)     |
| **Cache / Leaderboard** | Redis 7+ (StackExchange.Redis) |
| **Real-Time**           | SignalR                        |
| **Authentication**      | JWT Tokens                     |
| **Validation**          | FluentValidation               |
| **Documentation**       | Swagger / OpenAPI              |
| **Mapping**             | AutoMapper (optional)          |

---

## ⚙️ Getting Started

### Prerequisites

* .NET 8.0 SDK
* PostgreSQL 15+
* Redis 7+
* Visual Studio 2022 / VS Code

### Installation

```bash
# Clone the repository
git clone https://github.com/yourusername/LeaderboardSystem.git
cd LeaderboardSystem

# Update configuration
# Edit appsettings.json with your DB and Redis credentials

# Run migrations
dotnet ef database update --project src/LeaderboardSystem.Infrastructure

# Start the application
dotnet run --project src/LeaderboardSystem.API
```

---

## 🧾 Configuration Example

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=leaderboard;Username=postgres;Password=yourpassword",
    "Redis": "localhost:6379"
  },
  "JwtSettings": {
    "Secret": "your-256-bit-secret-key-here",
    "Issuer": "LeaderboardSystem",
    "Audience": "LeaderboardUsers",
    "ExpiryInMinutes": 60
  }
}
```

---

## 📡 API Endpoints

### 🔐 Authentication

* `POST /api/auth/register` — Register a new user
* `POST /api/auth/login` — Log in and get JWT token

### 🏁 Scores

* `POST /api/scores` — Submit a new score
* `GET /api/scores/history` — Get user’s score history
* `GET /api/scores/rank` — Get current user rank

### 🏆 Leaderboards

* `GET /api/leaderboards/global` — Get global leaderboard (top 100)
* `GET /api/leaderboards/game/{gameId}` — Get leaderboard for a specific game
* `GET /api/leaderboards/top-players` — Generate top player reports

---

## 🔔 Real-Time SignalR Hub

| Event                        | Description                           |
| ---------------------------- | ------------------------------------- |
| **ReceiveLeaderboardUpdate** | Broadcasts when leaderboard changes   |
| **ReceiveRankChange**        | Notifies user when their rank updates |

**Hub URL:** `/hubs/leaderboard`

---

## 📈 Next Steps

* Add friend and messaging systems
* Implement daily/weekly leaderboard resets
* Add scheduled reports with Hangfire
* Introduce caching and monitoring improvements

---

## 🧑‍💻 License

This project is open source and available under the [MIT License](LICENSE).
