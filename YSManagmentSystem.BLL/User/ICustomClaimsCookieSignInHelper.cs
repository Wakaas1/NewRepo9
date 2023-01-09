using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace YSManagmentSystem.BLL.User
{
    public interface ICustomClaimsCookieSignInHelper<TIdentityUser> where TIdentityUser : IdentityUser
    {
        Task SignInUserAsync(TIdentityUser user, bool isPersistent, IEnumerable<Claim> customClaims);
    }
}