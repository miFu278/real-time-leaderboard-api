using LeaderboardSystem.Application.Scores.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Scores.Queries.GetUserRank
{
    public record GetUserRankQuery(
        Guid UserId,
        Guid? GameId = null
        ) : IRequest<UserRankDto?>;
}
