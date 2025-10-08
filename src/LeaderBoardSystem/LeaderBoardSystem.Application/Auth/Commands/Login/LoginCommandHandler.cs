using LeaderboardSystem.Application.Auth.DTOs;
using LeaderboardSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardSystem.Application.Auth.Commands.Login
{
    public class LoginCommandHandler(IApplicationDbContext context, IAuthService authService) : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IAuthService _authService = authService;


        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.EmailOrUsername ||
                                     u.Username == request.EmailOrUsername,
                                     cancellationToken);

            if (user == null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Account is inactive");

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
