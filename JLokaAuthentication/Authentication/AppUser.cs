using Microsoft.AspNetCore.Identity;

namespace JLokaAuthentication.Authentication
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string ProfilePicture {  get; set; } = string.Empty;
    }
}
