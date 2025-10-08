using LeaderBoardSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaderboardSystem.Infrastructure.Persistence.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.Description)
                .HasMaxLength(500);

            builder.Property(g => g.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(g => g.IsActive)
                .HasDefaultValue(true);

            builder.Property(g => g.CreatedAt)
                .IsRequired();

            builder.HasIndex(g => g.Name).IsUnique();

            builder.HasMany(g => g.Scores)
                .WithOne(s => s.Game)
                .HasForeignKey(s => s.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
