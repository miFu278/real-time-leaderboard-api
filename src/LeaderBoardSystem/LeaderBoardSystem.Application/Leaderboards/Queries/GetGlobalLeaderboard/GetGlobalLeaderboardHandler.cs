using LeaderboardSystem.Application.Common.Interfaces;
using LeaderboardSystem.Application.Leaderboards.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Leaderboards.Queries.GetGlobalLeaderboard
{
    internal class GetGlobalLeaderboardHandler(ILeaderboardService leaderboardService) :
        IRequestHandler<GetGlobalLeaderboardQuery, List<LeaderboardEntryDto>>
    {
        private readonly ILeaderboardService _leaderboardService = leaderboardService;

        public async Task<List<LeaderboardEntryDto>> Handle(GetGlobalLeaderboardQuery request, CancellationToken cancellationToken)
        {
            var leaderboard = await _leaderboardService.GetGlobalLeaderboardAsync(request.Top100);

            return [.. leaderboard.Select(entry => new LeaderboardEntryDto
            {
                UserId = Guid.Parse(entry.UserId),
                Username = entry.Username,
                Score = entry.Score,
                Rank = entry.Rank,
                LastUpdated = entry.LastUpdated
            })];
        }
    }
}
