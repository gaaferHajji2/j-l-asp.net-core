using System.ComponentModel.DataAnnotations;

namespace JLokaAuthentication.Authentication
{
    public class AddOrUpdateAppUserModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = String.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = String.Empty;
        [EmailAddress]
        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; } = String.Empty;
    }
}
