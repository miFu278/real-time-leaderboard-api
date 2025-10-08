namespace LeaderboardSystem.Application.Leaderboards.DTOs
{
    public class LeaderboardEntryDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Score { get; set; }
        public int Rank { get; set; }
        public Guid? GameId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
