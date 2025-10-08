using LeaderBoardSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaderboardSystem.Infrastructure.Persistence.Configurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.ToTable("Scores");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Points)
                .IsRequired();

            builder.Property(s => s.SubmittedAt)
                .IsRequired();

            builder.Property(s => s.Metadata)
                .HasMaxLength(1000);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.HasIndex(s => new { s.UserId, s.GameId, s.SubmittedAt });
            builder.HasIndex(s => s.SubmittedAt);
            builder.HasIndex(s => s.Points);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Scores)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Game)
                .WithMany(g => g.Scores)
                .HasForeignKey(s => s.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

