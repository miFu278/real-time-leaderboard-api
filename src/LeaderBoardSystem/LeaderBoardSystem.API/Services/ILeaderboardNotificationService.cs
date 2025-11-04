using LeaderboardSystem.Application.Leaderboards.DTOs;

namespace LeaderboardSystem.API.Services
{
    public interface ILeaderboardNotificationService
    {
        Task NotifyGlobalLeaderboardUpdate(List<LeaderboardEntryDto> leaderboard);
        Task NotifyGameLeaderboardUpdate(Guid gameId, List<LeaderboardEntryDto> leaderboard);
        Task NotifyRankChange(Guid userId, string username, int newRank, int? oldRank = null);
    }
}
