using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RegistraWebApi.Controllers
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public  IActionResult GetValues()
        {
            return Ok(Summaries);
            
        }

        [Authorize(Roles = "Client")]
        [HttpGet("{id}")]
        public  IActionResult Get(int id)
        {
            return Ok(Summaries[id]);
        }
    }
}
