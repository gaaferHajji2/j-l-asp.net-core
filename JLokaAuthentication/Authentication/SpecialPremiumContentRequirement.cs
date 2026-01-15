using Microsoft.AspNetCore.Authorization;

namespace JLokaAuthentication.Authentication
{
    public class SpecialPremiumContentRequirement : IAuthorizationRequirement
    {
        public string Country { get; set; } = string.Empty;

        public SpecialPremiumContentRequirement(string country)
        {
            Country = country;
        }
    }
}
