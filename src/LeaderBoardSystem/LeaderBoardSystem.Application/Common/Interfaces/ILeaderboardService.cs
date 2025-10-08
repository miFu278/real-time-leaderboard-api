using LeaderBoardSystem.Domain.Entities;

namespace LeaderboardSystem.Application.Common.Interfaces
{
    public interface ILeaderboardService
    {
        Task UpadteLeaderboardAsync(Guid userId, string username, Guid gameId, int points);
        Task<IEnumerable<LeaderBoardEntry>> GetGlobalLeaderboardAsync(int top = 100);
        Task<IEnumerable<LeaderBoardEntry>> GetGameLeaderBoardAsync(Guid gameId, int top = 100);
        Task<(int Rank, int Score)?> GetUserRankAsync(Guid userId, Guid? gameId = null);
    }
}
