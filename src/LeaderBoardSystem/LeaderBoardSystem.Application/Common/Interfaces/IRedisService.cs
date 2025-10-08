namespace LeaderboardSystem.Application.Common.Interfaces
{
    public interface IRedisService
    {
        Task<bool> AddScoreAsync(string key, string member, double score);
        Task<long> GetRankAsync(string key, string member, bool descending = true);
        Task<double?> GetScoreAsync(string key, string member);
        Task<IEnumerable<(string Member, double Score)>> GetTopRangeAsync(string key, int start, int stop, bool descending = true);
        Task<IEnumerable<(string Member, double Score, long Rank)>> GetTopWithRanksAsync(string key, int start, int stop);
        Task<long> GetLeaderboardSizeAsync(string key);
        Task<bool> RemoveMemberAsync(string key, string member);
    }
}
