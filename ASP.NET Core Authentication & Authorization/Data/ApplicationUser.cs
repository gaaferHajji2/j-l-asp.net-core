using Microsoft.AspNetCore.Identity;

namespace ASP.NET_Core_Authentication___Authorization.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
