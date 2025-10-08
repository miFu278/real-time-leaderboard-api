namespace LeaderboardSystem.Application.Leaderboards.DTOs
{
    public class TopPlayerReportDto
    {
        public int Rank { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int TotalScore { get; set; }
        public int GamePlayed { get; set; }
        public int TotalGames { get; set; }
        public int AverageScore { get; set; }
        public int HighestScore { get; set; }

    }
}
