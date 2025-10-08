using LeaderboardSystem.Application.Common.Interfaces;

namespace LeaderboardSystem.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
