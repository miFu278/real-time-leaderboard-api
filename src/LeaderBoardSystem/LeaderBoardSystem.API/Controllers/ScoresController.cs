using Asp.Versioning;
using LeaderboardSystem.Application.Scores.Commands.SubmitScore;
using LeaderboardSystem.Application.Scores.Queries.GetUserRank;
using LeaderboardSystem.Application.Scores.Queries.GetUserScoreHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LeaderboardSystem.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScoresController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> SubmitScore([FromBody] SubmitScoreRequest request)
        {
            var userId = GetUserId();
            var command = new SubmitScoreCommand(userId, request.GameId, request.Points, request.Metadata);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory(
            [FromQuery] Guid? gameId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var userId = GetUserId();
            var query = new GetUserScoreHistoryQuery(userId, gameId, pageNumber, pageSize);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("rank")]
        public async Task<IActionResult> GetRank([FromQuery] Guid? gameId = null)
        {
            var userId = GetUserId();
            var query = new GetUserRankQuery(userId, gameId);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound("User not found in leaderboard");

            return Ok(result);

        }


        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User ID not found in token");

            return Guid.Parse(userIdClaim);
        }
    }

    public record SubmitScoreRequest(Guid GameId, int Points, string? Metadata = null);
}
