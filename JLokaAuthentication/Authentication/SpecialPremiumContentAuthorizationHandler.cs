using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JLokaAuthentication.Authentication
{
    public class SpecialPremiumContentAuthorizationHandler: AuthorizationHandler<SpecialPremiumContentRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SpecialPremiumContentRequirement requirement)
        {
            var hasPremiumAccess = context.User.HasClaim(c => c.Type == AppClaimTypes.Subscription && c.Value == "Premium");
            if(!hasPremiumAccess)
            {
                return Task.CompletedTask;
            }
            var countryClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Country);
            if (countryClaim == null || String.IsNullOrEmpty(countryClaim.ToString())) 
            {
                return Task.CompletedTask;
            }

            if(countryClaim.Value == requirement.Country)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
