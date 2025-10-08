using LeaderboardSystem.Application.Auth.DTOs;
using MediatR;

namespace LeaderboardSystem.Application.Auth.Commands.Register
{
    public record RegisterCommand(
        string Username,
        string Email,
        string Password
        ) : IRequest<AuthResponseDto>;
}
