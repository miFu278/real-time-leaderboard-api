using LeaderboardSystem.Application.Common.Exceptions;
using LeaderboardSystem.Application.Common.Interfaces;
using LeaderboardSystem.Application.Scores.DTOs;
using LeaderBoardSystem.Domain.Entities;
using MediatR;

namespace LeaderboardSystem.Application.Scores.Commands.SubmitScore
{
    public class SubmitScoreCommandHandler(IApplicationDbContext context,
        ILeaderboardService leaderboardService, IDateTime dateTime) : IRequestHandler<SubmitScoreCommand, ScoreDto>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly ILeaderboardService _leaderboardService = leaderboardService;
        private readonly IDateTime _dateTime = dateTime;

        public async Task<ScoreDto> Handle(SubmitScoreCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync([request.UserId], cancellationToken) ??
                throw new NotFoundException(nameof(User), request.UserId);
            var game = await _context.Games.FindAsync([request.GameId], cancellationToken) ??
                throw new NotFoundException(nameof(Game), request.GameId);

            var score = new Score
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                GameId = request.GameId,
                Points = request.Points,
                SubmittedAt = _dateTime.UtcNow,
                Metadata = request.Metadata,
                CreatedAt = _dateTime.UtcNow
            };

            _context.Scores.Add(score);
            await _context.SaveChangeAsync(cancellationToken);

            await _leaderboardService.UpadteLeaderboardAsync(user.Id, user.Username, game.Id, request.Points);

            return new ScoreDto
            {
                Id = score.Id,
                UserId = score.UserId,
                Username = user.Username,
                GameId = score.Id,
                GameName = game.Name,
                Points = score.Points,
                SubmittedAt = score.SubmittedAt
            };
        }
    }
}
