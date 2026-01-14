using JLokaAuthentication.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JLokaAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(UserManager<AppUser> userManager, IConfiguration _configuration) : ControllerBase
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
                var userResult = await userManager.CreateAsync(user, model.Password);

                // Adding the user to "User" Role
                var roleResult = await userManager.AddToRoleAsync(user, AppRoles.User);

                // if the result is success return the result
                if (userResult.Succeeded)
                {
                    var token = GenerateToken(model.UserName, user);
                    return Ok(new { token });
                }

                // else return the errors
                foreach (var error in userResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        private async Task<string?> GenerateToken(string userName, AppUser user)
        {
            var secret = _configuration["JwtConfig:Secret"];
            var issuer = _configuration["JwtConfig:ValidIssuer"];
            var audience = _configuration["JwtConfig:ValidAudiences"];

            if(secret is null || issuer is null || audience is null)
            {
                throw new ApplicationException("Jwt is not set in the configuration");
            }
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var userRoles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userName)
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Get the secret in the configuration
            // Check if the model is valid
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if(user != null)
                {
                    if (await userManager.CheckPasswordAsync(user, model.password))
                    {
                        var token = GenerateToken(model.Username, user);
                        return Ok(new { token });
                    }
                }
                // If the user is not found, display an error message
                ModelState.AddModelError("", "Invalid username or password");
            }
            return BadRequest(ModelState);
        }
    }
}
