using LeaderboardSystem.Application.Common.Interfaces;
using LeaderboardSystem.Application.Scores.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardSystem.Application.Scores.Queries.GetUserScoreHistory
{
    public class GetUserScoreHistoryQueryHandler(IApplicationDbContext context) : IRequestHandler<GetUserScoreHistoryQuery, List<ScoreDto>>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<ScoreDto>> Handle(GetUserScoreHistoryQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Scores
                .Include(s => s.User)
                .Include(s => s.Game)
                .Where(s => s.UserId == request.UserId);

            if (request.GameId.HasValue)
                query = query.Where(s => s.GameId == request.GameId.Value);

            var scores = await query
                .OrderByDescending(s => s.SubmittedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new ScoreDto
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Username = s.User.Username,
                    GameId = s.GameId,
                    GameName = s.Game.Name,
                    Points = s.Points,
                    SubmittedAt = s.SubmittedAt
                })
                .ToListAsync(cancellationToken);

            return scores;
        }
    }
}
