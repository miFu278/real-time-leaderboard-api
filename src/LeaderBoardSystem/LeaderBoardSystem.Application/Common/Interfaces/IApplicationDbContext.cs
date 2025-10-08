using LeaderBoardSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardSystem.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Game> Games { get; }
        DbSet<Score> Scores { get; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
