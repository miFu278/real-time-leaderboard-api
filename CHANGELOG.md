# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

### Added
- ✅ FluentValidation for request validation
  - RegisterCommandValidator
  - LoginCommandValidator  
  - SubmitScoreCommandValidator
- ✅ Health Checks for PostgreSQL and Redis
- ✅ Serilog for structured logging
- ✅ Rate limiting middleware (100 requests/minute)
- ✅ API Versioning (v1.0)
- ✅ Docker and docker-compose.yml
- ✅ .dockerignore file
- ✅ Unit Test project with FluentAssertions and Moq
- ✅ Integration Test project with WebApplicationFactory
- ✅ CI/CD GitHub Actions workflows
- ✅ Multiple environment configurations (Development, Production)
- ✅ .env.example template
- ✅ MIT License
- ✅ SETUP.md documentation
- ✅ Database migrations

### Fixed
- ✅ Fixed file name with trailing space in LeaderboardsController
- ✅ Updated EF Core packages to version 9.0.10 for consistency
- ✅ Removed invalid Microsoft.AspNetCore.RateLimiting package (using built-in .NET 8 rate limiting)

### Changed
- ✅ Improved CORS configuration with separate policies for API and SignalR
- ✅ Enhanced Program.cs with proper error handling and logging
- ✅ Updated all controllers to use API versioning with v1 routes

## [0.1.0] - 2025-11-04

### Initial Release
- Initial project structure with Clean Architecture
- CQRS pattern with MediatR
- JWT authentication
- Real-time updates with SignalR
- Redis integration for leaderboards
- PostgreSQL with Entity Framework Core
- Swagger/OpenAPI documentation
