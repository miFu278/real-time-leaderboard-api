namespace LeaderboardSystem.Application.Scores.DTOs
{
    public class ScoreDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public Guid GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
