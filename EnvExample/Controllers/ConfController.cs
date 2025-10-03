using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfController(IConfiguration configuration) => _configuration = configuration;

        [HttpGet]
        [Route("test-01")]
        public ActionResult GetConf()
        {
            var type = _configuration["Database:Type"];
            var connStr = _configuration["Database:ConnectionString"];

            return Ok(new {
                Type= type, ConnStr= connStr 
            });
        }
    }
}