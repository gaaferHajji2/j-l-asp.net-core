using System.ComponentModel.DataAnnotations;

namespace JLokaAuthentication.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = String.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; } = string.Empty;
    }
}
