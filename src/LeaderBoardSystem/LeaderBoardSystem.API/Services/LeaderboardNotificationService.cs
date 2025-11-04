using LeaderboardSystem.API.Hubs;
using LeaderboardSystem.Application.Leaderboards.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace LeaderboardSystem.API.Services
{
    public class LeaderboardNotificationService(IHubContext<LeaderboardHub> hubContext) : ILeaderboardNotificationService
    {
        private readonly IHubContext<LeaderboardHub> _hubContext = hubContext;

        public async Task NotifyGameLeaderboardUpdate(Guid gameId, List<LeaderboardEntryDto> leaderboard)
        {
            await _hubContext.Clients.Group($"game:{gameId}")
                .SendAsync("ReceiveLeaderboardUpdate", new
                {
                    Type = "game",
                    GameId = gameId,
                    Leaderboard = leaderboard,
                    UpdatedAt = DateTime.UtcNow
                });
        }

        public async Task NotifyGlobalLeaderboardUpdate(List<LeaderboardEntryDto> leaderboard)
        {
            await _hubContext.Clients.Group("global")
                .SendAsync("ReceiveLeaderboardUpdate", new
                {
                    Type = "global",
                    Leaderboard = leaderboard,
                    UpdatedAt = DateTime.UtcNow
                });
        }

        public async Task NotifyRankChange(Guid userId, string username, int newRank, int? oldRank = null)
        {
            await _hubContext.Clients.All
                .SendAsync("ReceiveRankChange", new
                {
                    UserId = userId,
                    Username = username,
                    NewRank = newRank,
                    OldRank = oldRank,
                    UpdatedAt = DateTime.UtcNow
                });
        }
    }
}
