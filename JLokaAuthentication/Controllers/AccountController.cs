using JLokaAuthentication.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JLokaAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(UserManager<AppUser> userManager, IConfiguration configuration) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddOrUpdateAppUserModel model)
        {
            // check if the data is valid
            if (ModelState.IsValid)
            {
                var existedUser = await userManager.FindByNameAsync(model.UserName);
                if (existedUser != null)
                {
                    ModelState.AddModelError("", "Username already exists");
                    return BadRequest(ModelState);
                }

                // create the user model
                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                // try to save the user with password
                var result = await userManager.CreateAsync(user, model.Password);

                // if the result is success return the result
                if (result.Succeeded)
                {
                    var token = GenerateToken(model.UserName);
                    return Ok(new { token });
                }

                // else return the errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return BadRequest(ModelState);
        }

        private object GenerateToken(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
