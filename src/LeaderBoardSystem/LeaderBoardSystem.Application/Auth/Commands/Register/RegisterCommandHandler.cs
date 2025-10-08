using LeaderboardSystem.Application.Auth.DTOs;
using LeaderboardSystem.Application.Common.Interfaces;
using LeaderBoardSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardSystem.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler(IApplicationDbContext context, IAuthService authService, IDateTime dateTime) : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IAuthService _authService = authService;
        private readonly IDateTime _dateTime = dateTime;

        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(
                u => u.Email == request.Email ||
                u.Username == request.Username,
                cancellationToken))
                throw new InvalidOperationException("User with this email or username already existes");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _authService.HashPassword(request.Password),
                Role = "User",
                IsActive = true,
                CreatedAt = _dateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangeAsync(cancellationToken);

            var token = _authService.GenerateJwtToken(user.Id, user.Username, user.Role);

            return new AuthResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }
    }
}
