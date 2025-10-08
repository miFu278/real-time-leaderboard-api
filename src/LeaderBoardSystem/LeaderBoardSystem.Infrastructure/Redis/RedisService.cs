using LeaderboardSystem.Application.Common.Interfaces;
using StackExchange.Redis;

namespace LeaderboardSystem.Infrastructure.Redis
{
    public class RedisService(RedisConnectionFactory connectionFactory) : IRedisService
    {
        private readonly StackExchange.Redis.IDatabase _database = connectionFactory.GetDatabase();

        public async Task<bool> AddScoreAsync(string key, string member, double score)
            => await _database.SortedSetAddAsync(key, member, score);

        public async Task<bool> RemoveMemberAsync(string key, string member)
            => await _database.SortedSetRemoveAsync(key, member);

        public async Task<long> GetLeaderboardSizeAsync(string key)
            => await _database.SortedSetLengthAsync(key);

        public async Task<long> GetRankAsync(string key, string member, bool descending = true)
        {
            var rank = descending
                ? await _database.SortedSetRankAsync(key, member, StackExchange.Redis.Order.Descending)
                : await _database.SortedSetRankAsync(key, member, StackExchange.Redis.Order.Ascending);

            return rank ?? -1;
        }

        public async Task<double?> GetScoreAsync(string key, string member)
            => await _database.SortedSetScoreAsync(key, member);

        public async Task<IEnumerable<(string Member, double Score)>> GetTopRangeAsync(string key, int start, int stop, bool descending = true)
        {
            var order = descending ? Order.Descending : Order.Ascending;

            var entries = await _database.SortedSetRangeByRankWithScoresAsync(key, start, stop, order);

            return entries.Select(e => (e.Element.ToString(), e.Score));
        }

        public async Task<IEnumerable<(string Member, double Score, long Rank)>> GetTopWithRanksAsync(string key, int start, int stop)
        {
            var entries = await _database.SortedSetRangeByRankWithScoresAsync(
                key, start, stop, Order.Descending);

            var result = new List<(string Member, double Score, long Rank)>();
            long rank = start;

            foreach (var entry in entries)
            {
                result.Add((entry.Element.ToString(), entry.Score, rank));
                rank++;
            }

            return result;
        }

    }
}
