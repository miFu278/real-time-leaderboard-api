using LeaderboardSystem.Application.Common.Interfaces;
using LeaderboardSystem.Application.Leaderboards.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Leaderboards.Queries.GetGameLeaderboard
{
    public class GetGameLeaderboardHandler(ILeaderboardService leaderboardService) :
        IRequestHandler<GetGameLeaderboardQuery, List<LeaderboardEntryDto>>
    {
        private readonly ILeaderboardService _leaderboardService = leaderboardService;

        public async Task<List<LeaderboardEntryDto>> Handle(GetGameLeaderboardQuery request, CancellationToken cancellationToken)
        {
            var leaderboard = await _leaderboardService.GetGameLeaderBoardAsync(request.GameId, request.Top100);

            return [.. leaderboard.Select(entry => new LeaderboardEntryDto {
                UserId = Guid.Parse(entry.UserId),
                Username = entry.Username,
                Score = entry.Score,
                Rank = entry.Rank,
                GameId = request.GameId,
                LastUpdated = entry.LastUpdated
            })];
        }
    }
}
