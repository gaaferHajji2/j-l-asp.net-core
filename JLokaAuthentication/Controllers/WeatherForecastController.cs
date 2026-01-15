using JLokaAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JLokaAuthentication.Controllers
{
    //[Authorize(Roles = AppRoles.Administrator)]
    [Authorize(Roles = $"{AppRoles.Administrator},{AppRoles.User}")]
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

        [HttpGet("user", Name = "GetWeatherForecast")]
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

        // In This way it must contains all roles
        // to access this api
        [HttpGet("Vip", Name = "GetWeatherForecastVip")]
        [Authorize(Roles = AppRoles.User)]
        [Authorize(Roles = AppRoles.VipUser)]
        public IEnumerable<WeatherForecast> GetVip()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("user-with-policy", Name = "GetWeatherForecastByPolicy")]
        [Authorize(Policy = "RequireUserRole")]
        public IEnumerable<WeatherForecast> GetByPolicy()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("admin-with-policy", Name = "GetWeatherForecastByAdminPolicy")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public IEnumerable<WeatherForecast> GetByAdminPolicy()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize(Policy = AppAuthorizationPolicies.RequireDrivingLicenseNumber)]
        [HttpGet("driving-license")]
        public IActionResult GetDrivingLicenseNumber() 
        {
            var drivingLicenseNumber = User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.DrivingLicenseNumber)?.Value;
            return Ok(new { drivingLicenseNumber });
        }

        [Authorize(Policy = AppAuthorizationPolicies.RequireCountry)]
        [HttpGet("country")]
        public IActionResult GetCountry()
        {
            var country = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country)?.Value;
            return Ok(new { country }); 
        }

        [Authorize(Policy = AppAuthorizationPolicies.RequireDrivingLicenseAndAccessNumber)]
        [HttpGet("driving-license-and-access-number")]
        public IActionResult GetNoContent()
        {
            return NoContent();
        }

        [Authorize(Policy = AppAuthorizationPolicies.SpecialPremiumContent)]
        [HttpGet("get-premium")]
        public IActionResult GetPremium()
        {
            return NoContent();
        }
    }
}
