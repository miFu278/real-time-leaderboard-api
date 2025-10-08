using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        //[HttpPost]
        //public async Task<IActionResult> SubmitScore([FromBody] SubmitScoreRequest request)
        //{

        //}
    }

    public record SubmitScoreRequest(Guid GameId, int Points, string? Metadata = null);
}
