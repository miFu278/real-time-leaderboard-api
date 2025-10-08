using LeaderboardSystem.Application.Leaderboards.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Leaderboards.Queries.GetGameLeaderboard
{
    public record GetGameLeaderboardQuery(
        Guid GameId,
        int Top100
        ) : IRequest<List<LeaderboardEntryDto>>;
}
