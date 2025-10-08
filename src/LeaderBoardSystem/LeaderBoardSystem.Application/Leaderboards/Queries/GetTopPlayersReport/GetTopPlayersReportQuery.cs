using LeaderboardSystem.Application.Leaderboards.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Leaderboards.Queries.GetTopPlayersReport
{
    public record GetTopPlayersReportQuery(
        DateTime? StartDate = null, DateTime? EndDate = null, int Top = 10
        ) : IRequest<List<TopPlayerReportDto>>;
}
