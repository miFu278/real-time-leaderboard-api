using LeaderboardSystem.Application.Auth.Commands.Login;
using LeaderboardSystem.Application.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = new RegisterCommand(request.Username, request.Email, request.Password);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand(request.EmailOrUsername, request.Password);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }

    public record RegisterRequest(string Username, string Email, string Password);
    public record LoginRequest(string EmailOrUsername, string Password);
}
