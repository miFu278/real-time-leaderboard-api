# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["src/LeaderBoardSystem/LeaderboardSystem.sln", "src/LeaderBoardSystem/"]
COPY ["src/LeaderBoardSystem/LeaderboardSystem.API/LeaderboardSystem.API.csproj", "src/LeaderBoardSystem/LeaderboardSystem.API/"]
COPY ["src/LeaderBoardSystem/LeaderboardSystem.Application/LeaderboardSystem.Application.csproj", "src/LeaderBoardSystem/LeaderboardSystem.Application/"]
COPY ["src/LeaderBoardSystem/LeaderboardSystem.Domain/LeaderboardSystem.Domain.csproj", "src/LeaderBoardSystem/LeaderboardSystem.Domain/"]
COPY ["src/LeaderBoardSystem/LeaderboardSystem.Infrastructure/LeaderboardSystem.Infrastructure.csproj", "src/LeaderBoardSystem/LeaderboardSystem.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "src/LeaderBoardSystem/LeaderboardSystem.API/LeaderboardSystem.API.csproj"

# Copy source code
COPY . .

# Build
WORKDIR "/src/src/LeaderBoardSystem/LeaderboardSystem.API"
RUN dotnet build "LeaderboardSystem.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "LeaderboardSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LeaderboardSystem.API.dll"]
