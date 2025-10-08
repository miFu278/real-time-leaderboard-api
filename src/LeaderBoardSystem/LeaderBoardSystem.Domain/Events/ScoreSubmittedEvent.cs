namespace LeaderboardSystem.Domain.Events
{
    public class ScoreSubmittedEvent
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid GameId { get; set; }
        public int Points { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
