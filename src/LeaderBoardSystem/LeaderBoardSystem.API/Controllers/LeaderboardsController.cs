using Asp.Versioning;
using LeaderboardSystem.Application.Leaderboards.Queries.GetGameLeaderboard;
using LeaderboardSystem.Application.Leaderboards.Queries.GetGlobalLeaderboard;
using LeaderboardSystem.Application.Leaderboards.Queries.GetTopPlayersReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardSystem.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LeaderboardsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalLeaderboard([FromQuery] int top = 100)
        {
            var query = new GetGlobalLeaderboardQuery(top);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetGameLeaderboard(Guid gameId, [FromQuery] int top = 100)
        {
            var query = new GetGameLeaderboardQuery(gameId, top);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("top-players")]
        public async Task<IActionResult> GetTopPlayersReport(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int top = 10)
        {
            var query = new GetTopPlayersReportQuery(startDate, endDate, top);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
