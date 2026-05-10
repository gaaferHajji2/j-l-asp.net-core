using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Authentication___Authorization.DTOs.Request
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; }= string.Empty;
    }
}
