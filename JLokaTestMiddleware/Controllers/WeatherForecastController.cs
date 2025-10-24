using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using System;

namespace JLokaTestMiddleware.Controllers
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

        [HttpPost(Name ="GetHello")]
        [EnableRateLimiting(policyName: "fixed")]
        public ActionResult getHello()
        {
            return Ok("Hello Jafar Loka Rate Limiter World!!!");
        }

        [HttpPut(Name = "Check time out")]
        [RequestTimeout(5000)]

        public async Task<ActionResult> checkTimeout() {
            var _random = new Random();
            var delay = _random.Next(1, 10);
            _logger.LogInformation($"Delaying for {delay} seconds");
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(delay), Request.HttpContext.RequestAborted);
            }
            catch
            {
                _logger.LogWarning("The request timed out");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "The request timed out");
            }
            return Ok($"Hello! The task is complete in {delay} seconds");
        }
    }
}
