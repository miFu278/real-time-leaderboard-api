using LeaderboardSystem.Application.Leaderboards.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Leaderboards.Queries.GetGlobalLeaderboard
{
    public record GetGlobalLeaderboardQuery(
        int Top100
        ) : IRequest<List<LeaderboardEntryDto>>;
}
