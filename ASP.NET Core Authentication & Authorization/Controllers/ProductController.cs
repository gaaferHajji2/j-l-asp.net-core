using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Authentication___Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Public endpoint
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Ok();
        }

        // Authenticated users only
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) 
        { 
            return Ok(); 
        }

        // Admins only
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create() 
        {
            return Ok();
        }

        // Policy-based authorization
        [Authorize(Policy = "CanManageUsers")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) 
        { 
            return Ok(); 
        }
    }
}
