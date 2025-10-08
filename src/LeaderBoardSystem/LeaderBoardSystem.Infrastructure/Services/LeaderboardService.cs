using LeaderboardSystem.Application.Common.Interfaces;
using LeaderBoardSystem.Domain.Entities;

namespace LeaderboardSystem.Infrastructure.Services
{
    public class LeaderboardService(IRedisService redisService, IDateTime dateTime) : ILeaderboardService
    {
        private readonly IRedisService _redisSerivce = redisService;
        private readonly IDateTime _dateTime = dateTime;
        private const string GlobalLeaderboardKey = "leaderboard:global";
        private const string GameLeaderboardPrefix = "leaderboard:game";

        public async Task<IEnumerable<LeaderBoardEntry>> GetGameLeaderBoardAsync(Guid gameId, int top = 100)
        {
            var gameKey = $"{GameLeaderboardPrefix}{gameId}";
            var entries = await _redisSerivce.GetTopWithRanksAsync(gameKey, 0, top - 1);

            return entries.Select(e =>
            {
                var parts = e.Member.Split(':');
                return new LeaderBoardEntry
                {
                    UserId = parts[0],
                    Username = parts.Length > 1 ? parts[1] : parts[0],
                    Score = (int)e.Score,
                    Rank = (int)e.Rank + 1,
                    LastUpdated = _dateTime.UtcNow
                };
            });
        }

        public async Task<IEnumerable<LeaderBoardEntry>> GetGlobalLeaderboardAsync(int top = 100)
        {
            var entries = await _redisSerivce.GetTopWithRanksAsync(GameLeaderboardPrefix, 0, top - 1);

            return entries.Select(e =>
            {
                var parts = e.Member.Split(':');
                return new LeaderBoardEntry
                {
                    UserId = parts[0],
                    Username = parts.Length > 1 ? parts[1] : parts[0],
                    Score = (int)e.Score,
                    Rank = (int)e.Rank + 1,
                    LastUpdated = _dateTime.UtcNow
                };
            });
        }

        public async Task<(int Rank, int Score)?> GetUserRankAsync(Guid userId, Guid? gameId = null)
        {
            var key = gameId.HasValue
                ? $"{GameLeaderboardPrefix}{gameId}"
                : GlobalLeaderboardKey;

            var allEntries = await _redisSerivce.GetTopRangeAsync(key, 0, -1);
            var userEntry = allEntries.FirstOrDefault(e => e.Member.StartsWith(userId.ToString()));

            if (userEntry.Member == null)
                return null;

            var rank = await _redisSerivce.GetRankAsync(key, userEntry.Member);

            if (rank == -1)
                return null;

            return ((int)rank + 1, (int)userEntry.Score);
        }

        public async Task UpadteLeaderboardAsync(Guid userId, string username, Guid gameId, int points)
        {
            var userKey = $"{userId}:{username}";

            var currentGlobalScore = await _redisSerivce.GetScoreAsync(GlobalLeaderboardKey, userKey);
            var newGlobalScore = (currentGlobalScore ?? 0) + points;
            await _redisSerivce.AddScoreAsync(GlobalLeaderboardKey, userKey, newGlobalScore);
        }
    }
}
