using LeaderboardSystem.Application.Auth.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Auth.Commands.Login
{
    public record LoginCommand(
        string EmailOrUsername,
        string Password
        ) : IRequest<AuthResponseDto>;
}
