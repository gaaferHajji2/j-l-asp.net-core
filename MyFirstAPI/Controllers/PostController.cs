using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
