using FluentValidation;

namespace LeaderboardSystem.Application.Scores.Commands.SubmitScore
{
    public class SubmitScoreCommandValidator : AbstractValidator<SubmitScoreCommand>
    {
        public SubmitScoreCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.GameId).NotEmpty().WithMessage("Game ID is required");
            RuleFor(x => x.Points).GreaterThanOrEqualTo(0).WithMessage("Points must be non-negative");
        }
    }
}
