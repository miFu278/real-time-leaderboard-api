using LeaderboardSystem.Application.Common.Interfaces;
using LeaderBoardSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LeaderboardSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<User> Users => Set<User>();

        public DbSet<Game> Games => Set<Game>();

        public DbSet<Score> Scores => Set<Score>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
            => await base.SaveChangesAsync(cancellationToken);
    }
}
