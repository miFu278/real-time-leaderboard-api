using LeaderBoardSystem.Domain.Common;
using LeaderBoardSystem.Domain.Enums;

namespace LeaderBoardSystem.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public GameType Type { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Score> Scores { get; set; } = [];
    }
}