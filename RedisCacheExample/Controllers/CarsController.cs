using Microsoft.AspNetCore.Mvc;

namespace RedisCacheExample.Controllers
{
    public class CarsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
