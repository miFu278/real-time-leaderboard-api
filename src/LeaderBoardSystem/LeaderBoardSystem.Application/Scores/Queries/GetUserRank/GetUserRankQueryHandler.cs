using LeaderboardSystem.Application.Common.Interfaces;
using LeaderboardSystem.Application.Scores.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Scores.Queries.GetUserRank
{
    public class GetUserRankQueryHandler(ILeaderboardService leaderboardService) : IRequestHandler<GetUserRankQuery, UserRankDto?>
    {
        private readonly ILeaderboardService _leaderboardService = leaderboardService;

        public async Task<UserRankDto?> Handle(GetUserRankQuery request, CancellationToken cancellationToken)
        {
            var result = await _leaderboardService.GetUserRankAsync(request.UserId, request.GameId);

            if (result == null)
                return null!;

            return new UserRankDto
            {
                UserId = request.UserId,
                GameId = request.GameId,
                Rank = result.Value.Rank,
                Score = result.Value.Score
            };
        }
    }
}
