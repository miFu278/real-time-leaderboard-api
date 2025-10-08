namespace LeaderboardSystem.Application.Scores.DTOs
{
    public class UserRankDto
    {
        public Guid UserId { get; set; }
        public int Rank { get; set; }
        public int Score { get; set; }
        public Guid? GameId { get; set; }
    }
}
