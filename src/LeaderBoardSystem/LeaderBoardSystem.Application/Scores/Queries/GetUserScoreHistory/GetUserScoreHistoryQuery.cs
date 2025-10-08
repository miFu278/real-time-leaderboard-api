using LeaderboardSystem.Application.Scores.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Scores.Queries.GetUserScoreHistory
{
    public record GetUserScoreHistoryQuery(
        Guid UserId,
        Guid? GameId = null,
        int PageNumber = 1,
        int PageSize = 20
        ) : IRequest<List<ScoreDto>>;
}
