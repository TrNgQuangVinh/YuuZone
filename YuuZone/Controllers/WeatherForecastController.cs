using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Constant;
using Repositories.DTO.RequestDTO.User;
using Services.Service;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace YuuZone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize(Roles = "1")]
        [HttpGet("jwtRoles1")]
        [SwaggerOperation(Summary = "Should throw 401 Unauth if no jwt, 403 Forbid if role is not 1")]
        public async Task<IActionResult> JWTTestRoles()
        {
            var name = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var role = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value;
            var roleName = User.Claims.FirstOrDefault(x => x.Type == "RoleName")?.Value;
            return Ok($"Welcome back {role} - {name}, your role name is {roleName}");
        }

        [Authorize]
        [HttpGet("jwt")]
        [SwaggerOperation(Summary = "Should throw 401 Unauth if no jwt")]
        public async Task<IActionResult> JWTTest()
        {
            var name = User.Identity?.Name;
            return Ok($"Hello {name}");
        }
    }
    
}
