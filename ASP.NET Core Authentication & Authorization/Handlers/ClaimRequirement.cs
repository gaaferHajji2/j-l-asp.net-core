using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Core_Authentication___Authorization.Handlers
{
    public class ClaimRequirement : IAuthorizationRequirement
    {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public ClaimRequirement(string type, string value)
        {
            ClaimType = type;
            ClaimValue = value;
        }
    }

    public class ClaimRequirementHandler : AuthorizationHandler<ClaimRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ClaimRequirement requirement)
        {
            var claim = context.User.Claims.FirstOrDefault(c =>
                c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue);

            if (claim != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

}
