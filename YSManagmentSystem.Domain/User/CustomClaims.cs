using System.Security.Claims;
using System.Security.Principal;


namespace YSManagmentSystem.Domain.User
{
    public static class CustomClaims
    {
        public static Claim FindClaim(this IPrincipal user, string claimType)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(claimType)) throw new ArgumentNullException(nameof(claimType));
            var claimsPrincipal = user as ClaimsPrincipal;
            return claimsPrincipal?.FindFirst(claimType);
        }
        public static string Name(IPrincipal user)
        {
            return FindClaim(user, "Name")?.Value;
        }
        public static string Email(IPrincipal user)
        {
            return FindClaim(user, "Email")?.Value;
        }
        public static string Brand(IPrincipal user)
        {
            return FindClaim(user, "Brand")?.Value;
        }
        public static string Image(IPrincipal user)
        {
            string value = FindClaim(user, "Image")?.Value;
            return value;
        }
    }
}
