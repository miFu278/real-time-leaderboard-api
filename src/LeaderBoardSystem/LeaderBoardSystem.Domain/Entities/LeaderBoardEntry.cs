using LeaderBoardSystem.Domain.Common;

namespace LeaderBoardSystem.Domain.Entities
{
    public class LeaderBoardEntry : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int Score { get; set; }
        public int Rank { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
