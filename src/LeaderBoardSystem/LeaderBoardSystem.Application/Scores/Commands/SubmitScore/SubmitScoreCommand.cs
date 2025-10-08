using LeaderboardSystem.Application.Scores.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Scores.Commands.SubmitScore
{
    public record SubmitScoreCommand(
        Guid UserId,
        Guid GameId,
        int Points,
        string? Metadata = null
        ) : IRequest<ScoreDto>;
}
