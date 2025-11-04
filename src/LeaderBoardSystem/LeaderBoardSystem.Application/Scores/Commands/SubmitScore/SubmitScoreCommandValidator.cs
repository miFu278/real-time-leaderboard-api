using FluentValidation;

namespace LeaderboardSystem.Application.Scores.Commands.SubmitScore
{
    public class SubmitScoreCommandValidator : AbstractValidator<SubmitScoreCommand>
    {
        public SubmitScoreCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Game ID is required");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0).WithMessage("Points must be greater than or equal to 0")
                .LessThanOrEqualTo(1000000000).WithMessage("Points must not exceed 1,000,000,000");

            RuleFor(x => x.Metadata)
                .MaximumLength(1000).WithMessage("Metadata must not exceed 1000 characters")
                .When(x => !string.IsNullOrEmpty(x.Metadata));
        }
    }
}
