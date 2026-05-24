using ASP.NET_Core_Authentication___Authorization.Data;
using ASP.NET_Core_Authentication___Authorization.DTOs.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP.NET_Core_Authentication___Authorization.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserManagementController> _logger;

        public UserManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserManagementController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // === ROLE MANAGEMENT ===
        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return BadRequest(new { message = "Role already exists" });

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            _logger.LogInformation("Created role: {RoleName}", roleName);
            return Ok(new { message = "Role created" });
        }

        [HttpDelete("roles/{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded) return BadRequest(result.Errors);

            _logger.LogInformation("Deleted role: {RoleName}", roleName);
            return Ok(new { message = "Role deleted" });
        }

        // === USER ROLE ASSIGNMENT ===

        [HttpPost("users/{userId}/roles")]
        public async Task<IActionResult> AssignRoleToUser(
            string userId,
            [FromBody] string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (!await _roleManager.RoleExistsAsync(roleName))
                return BadRequest(new { message = "Role does not exist" });

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded) return BadRequest(result.Errors);

            _logger.LogInformation("Assigned role {RoleName} to user {UserId}", roleName, userId);
            return Ok(new { message = "Role assigned" });
        }

        [HttpDelete("users/{userId}/roles/{roleName}")]
        public async Task<IActionResult> RemoveRoleFromUser(
            string userId,
            string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded) return BadRequest(result.Errors);

            _logger.LogInformation("Removed role {RoleName} from user {UserId}", roleName, userId);
            return Ok(new { message = "Role removed" });
        }

        // === CLAIMS MANAGEMENT ===

        [HttpPost("users/{userId}/claims")]
        public async Task<IActionResult> AddClaimToUser(
            string userId,
            [FromBody] ClaimRequest claim)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var claims = await _userManager.GetClaimsAsync(user);
            var newClaim = new Claim(claim.Type, claim.Value);

            if (claims.Any(c => c.Type == newClaim.Type && c.Value == newClaim.Value))
            {
                return Conflict();
            }

            
            var result = await _userManager.AddClaimAsync(user, newClaim);

            if (!result.Succeeded) return BadRequest(result.Errors);

            _logger.LogInformation("Added claim {ClaimType}={ClaimValue} to user {UserId}",
                claim.Type, claim.Value, userId);
            return Ok(new { message = "Claim added" });
        }

        [HttpDelete("users/{userId}/claims")]
        public async Task<IActionResult> RemoveClaimFromUser(
            string userId,
            [FromBody] ClaimRequest claim)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var existingClaims = await _userManager.GetClaimsAsync(user);
            var claimToRemove = existingClaims.FirstOrDefault(c =>
                c.Type == claim.Type && c.Value == claim.Value);

            if (claimToRemove == null)
                return NotFound(new { message = "Claim not found on user" });

            var result = await _userManager.RemoveClaimAsync(user, claimToRemove);
            if (!result.Succeeded) return BadRequest(result.Errors);

            _logger.LogInformation("Removed claim {ClaimType}={ClaimValue} from user {UserId}",
                claim.Type, claim.Value, userId);
            return Ok(new { message = "Claim removed" });
        }

        // === GET USER DETAILS WITH ROLES & CLAIMS ===

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            return Ok(new
            {
                user.Id,
                user.Email,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.IsActive,
                Roles = roles,
                Claims = claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}
