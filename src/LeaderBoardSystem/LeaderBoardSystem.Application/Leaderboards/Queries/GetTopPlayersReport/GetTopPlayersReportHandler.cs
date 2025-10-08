using LeaderboardSystem.Application.Common.Interfaces;
using LeaderboardSystem.Application.Leaderboards.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardSystem.Application.Leaderboards.Queries.GetTopPlayersReport
{
    public class GetTopPlayersReportHandler(IApplicationDbContext context) : IRequestHandler<GetTopPlayersReportQuery, List<TopPlayerReportDto>>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<TopPlayerReportDto>> Handle(GetTopPlayersReportQuery request, CancellationToken cancellationToken)
        {
            var startDate = request.StartDate ?? DateTime.UtcNow.AddDays(-30);
            var endDate = request.EndDate ?? DateTime.UtcNow;

            var topPlayers = await _context.Scores
                    .Include(s => s.User)
                    .Where(s => s.SubmittedAt >= startDate && s.SubmittedAt <= endDate)
                    .GroupBy(s => new { s.UserId, s.User.Username })
                    .Select(g => new
                    {
                        g.Key.UserId,
                        g.Key.Username,
                        TotalScore = g.Sum(s => s.Points),
                        GamePlayed = g.Select(s => s.GameId).Distinct().Count(),
                        TotalGames = g.Count(),
                        AverageScore = g.Average(s => s.Points),
                        HighestScore = g.Max(s => s.Points)
                    })
                    .OrderByDescending(x => x.TotalScore)
                    .Take(request.Top)
                    .ToListAsync(cancellationToken: cancellationToken);

            var rank = 1;
            return [.. topPlayers.Select(p => new TopPlayerReportDto
            {
                Rank = rank++,
                UserId = p.UserId,
                Username = p.Username,
                TotalScore = p.TotalScore,
                GamePlayed = p.GamePlayed,
                TotalGames = p.TotalGames,
                AverageScore = (int)p.AverageScore,
                HighestScore = p.HighestScore
            })];
        }
    }
}
