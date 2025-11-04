# Setup Guide - Leaderboard System

Hướng dẫn chi tiết để setup và chạy Real-Time Leaderboard System.

## 📋 Yêu cầu hệ thống

- .NET 8.0 SDK
- PostgreSQL 15+
- Redis 7+
- Docker & Docker Compose (tùy chọn)
- Visual Studio 2022 / VS Code / Rider

## 🚀 Setup với Docker (Khuyên dùng)

### 1. Clone repository

```bash
git clone <repository-url>
cd real-time-leaderboard-api
```

### 2. Cấu hình môi trường

Tạo file `.env` từ template:

```bash
cp .env.example .env
```

Chỉnh sửa các giá trị trong file `.env`:

```env
POSTGRES_PASSWORD=your_secure_password
JWT_SECRET=your-super-secret-key-min-256-bits
```

### 3. Chạy với Docker Compose

```bash
# Build và start tất cả services
docker-compose up -d

# Xem logs
docker-compose logs -f api

# Stop services
docker-compose down
```

API sẽ chạy tại: `http://localhost:5000`

## 🔧 Setup thủ công (Local Development)

### 1. Cài đặt PostgreSQL

```bash
# Ubuntu/Debian
sudo apt install postgresql-15

# macOS
brew install postgresql@15

# Windows
# Download từ https://www.postgresql.org/download/
```

Tạo database:

```sql
CREATE DATABASE leaderboard_db;
CREATE USER leaderboard_user WITH PASSWORD 'your_password';
GRANT ALL PRIVILEGES ON DATABASE leaderboard_db TO leaderboard_user;
```

### 2. Cài đặt Redis

```bash
# Ubuntu/Debian
sudo apt install redis-server

# macOS
brew install redis

# Windows
# Download từ https://redis.io/download/
```

Start Redis:

```bash
redis-server
```

### 3. Cấu hình application

Sửa file `src/LeaderBoardSystem/LeaderboardSystem.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=leaderboard_db;Username=leaderboard_user;Password=your_password",
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

### 4. Restore dependencies

```bash
cd src/LeaderBoardSystem
dotnet restore
```

### 5. Chạy migrations

```bash
# Từ thư mục src/LeaderBoardSystem
dotnet ef database update --project LeaderboardSystem.Infrastructure --startup-project LeaderboardSystem.API

# Hoặc set connection string trực tiếp
dotnet ef database update --project LeaderboardSystem.Infrastructure --startup-project LeaderboardSystem.API --connection "Host=localhost;Database=leaderboard_db;Username=postgres;Password=postgres"
```

### 6. Chạy application

```bash
cd src/LeaderBoardSystem/LeaderboardSystem.API
dotnet run
```

API sẽ chạy tại:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

## 📝 Kiểm tra hoạt động

### Health Checks

```bash
# Overall health
curl http://localhost:5000/health

# Readiness
curl http://localhost:5000/health/ready

# Liveness
curl http://localhost:5000/health/live
```

### Swagger UI

Mở trình duyệt: `http://localhost:5000/swagger`

### API Endpoints

**Authentication:**
```bash
# Register
curl -X POST http://localhost:5000/api/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{"username":"testuser","email":"test@example.com","password":"Test@123!"}'

# Login
curl -X POST http://localhost:5000/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"emailOrUsername":"testuser","password":"Test@123!"}'
```

**SignalR Hub:**
- Hub URL: `/hubs/leaderboard`

## 🧪 Running Tests

### Unit Tests

```bash
cd src/LeaderBoardSystem
dotnet test LeaderboardSystem.UnitTests
```

### Integration Tests

Đảm bảo PostgreSQL và Redis đang chạy:

```bash
dotnet test LeaderboardSystem.IntegrationTests
```

### Chạy tất cả tests

```bash
dotnet test
```

## 🔐 Security

### JWT Secret

⚠️ **QUAN TRỌNG**: Đừng bao giờ commit JWT secret vào Git!

Tạo secret mạnh:

```bash
# Linux/macOS
openssl rand -base64 64

# PowerShell
[Convert]::ToBase64String((1..64 | ForEach-Object { Get-Random -Minimum 0 -Maximum 256 }))
```

### Database Credentials

- Sử dụng mật khẩu mạnh cho production
- Sử dụng environment variables hoặc Azure Key Vault
- Không hardcode credentials trong code

## 📊 Monitoring & Logging

### Logs

Logs được lưu tại:
- Console output
- Files: `logs/leaderboard-YYYYMMDD.log`

### Serilog Configuration

Chỉnh sửa trong `appsettings.json`:

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    }
  }
}
```

## 🐛 Troubleshooting

### "Unable to connect to PostgreSQL"

- Kiểm tra PostgreSQL đang chạy: `pg_isready`
- Kiểm tra connection string trong appsettings
- Kiểm tra firewall/port 5432

### "Unable to connect to Redis"

- Kiểm tra Redis đang chạy: `redis-cli ping`
- Kiểm tra port 6379
- Kiểm tra Redis password (nếu có)

### "Migration failed"

```bash
# Xóa migrations cũ
dotnet ef migrations remove --project LeaderboardSystem.Infrastructure --startup-project LeaderboardSystem.API

# Tạo lại migrations
dotnet ef migrations add InitialCreate --project LeaderboardSystem.Infrastructure --startup-project LeaderboardSystem.API
```

### "Port already in use"

Thay đổi port trong `appsettings.json` hoặc `launchSettings.json`

## 📚 Tài liệu bổ sung

- [README.md](README.md) - Tổng quan project
- [API Documentation](http://localhost:5000/swagger) - Swagger UI
- Architecture diagrams - Coming soon

## 🤝 Support

Nếu gặp vấn đề:
1. Kiểm tra logs trong `logs/` folder
2. Xem [GitHub Issues](https://github.com/yourusername/project/issues)
3. Tạo issue mới với thông tin chi tiết
