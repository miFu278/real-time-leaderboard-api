using LeaderBoardSystem.Domain.Common;

namespace LeaderBoardSystem.Domain.Entities
{
    public class Score : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid GameId { get; set; }
        public Game Game { get; set; } = null!;
        public int Points { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string? Metadata { get; set; }
    }
}