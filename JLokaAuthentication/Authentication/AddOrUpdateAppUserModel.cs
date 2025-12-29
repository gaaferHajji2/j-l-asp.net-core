using System.ComponentModel.DataAnnotations;

namespace JLokaAuthentication.Authentication
{
    public class AddOrUpdateAppUserModel
    {
        [Required(ErrorMessage = "Username is required")]
        public String UserName { get; set; } = String.Empty;
        [Required(ErrorMessage = "Password is required")]
        public String Password { get; set; } = String.Empty;
        [EmailAddress]
        [Required(ErrorMessage ="Email is Required")]
        public String Email { get; set; } = String.Empty;

    }
}
