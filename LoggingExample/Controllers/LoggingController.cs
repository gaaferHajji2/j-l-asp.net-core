using Microsoft.AspNetCore.Mvc;

namespace LoggingExample.Controllers
{
    public class LoggingController : Controller
    {

        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger) { 
            _logger = logger;   
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/log")]
        public ActionResult LogExample()
        {
            _logger.LogInformation("This is a logging message with args: Today  is { Week }. It is { Time }.", DateTime.Now.DayOfWeek, DateTime.Now.ToLongTimeString());
            _logger.LogInformation($"This is a logging message with string concatenation: Today is { DateTime.Now.DayOfWeek }. It is{DateTime.Now.ToLongTimeString()}.");
            return Ok("This is to test the difference between structured logging and string concatenation.");
        }
    }
}
