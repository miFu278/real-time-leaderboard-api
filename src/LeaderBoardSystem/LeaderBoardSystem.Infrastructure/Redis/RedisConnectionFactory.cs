using StackExchange.Redis;

namespace LeaderboardSystem.Infrastructure.Redis
{
    public class RedisConnectionFactory(string connectionString)
    {
        private readonly Lazy<ConnectionMultiplexer> _connection = new(() => ConnectionMultiplexer.Connect(connectionString));

        public IDatabase GetDatabase(int db = -1) => _connection.Value.GetDatabase(db);
        public IServer GetServer(string host, int port) => _connection.Value.GetServer(host, port);
    }
}
