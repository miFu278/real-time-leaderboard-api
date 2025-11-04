using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LeaderboardSystem.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HealthController(HealthCheckService healthCheckService) : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService = healthCheckService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var report = await _healthCheckService.CheckHealthAsync();

            var response = new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new
                {
                    name = e.Key,
                    status = e.Value.Status.ToString(),
                    description = e.Value.Description,
                    duration = e.Value.Duration.TotalMilliseconds
                }),
                totalDuration = report.TotalDuration.TotalMilliseconds
            };

            return report.Status == HealthStatus.Healthy
                ? Ok(response)
                : StatusCode(503, response);
        }

        [HttpGet("ready")]
        public IActionResult Ready()
        {
            return Ok(new { status = "Ready" });
        }

        [HttpGet("live")]
        public IActionResult Live()
        {
            return Ok(new { status = "Live" });
        }
    }
}
