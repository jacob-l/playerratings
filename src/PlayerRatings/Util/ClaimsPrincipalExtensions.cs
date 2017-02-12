using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PlayerRatings.Models;

namespace PlayerRatings.Util
{
    public static class ClaimsPrincipalExtensions
    {
        public static async Task<ApplicationUser> GetApplicationUser(this ClaimsPrincipal user, UserManager<ApplicationUser> userManager)
        {
            return await userManager.GetUserAsync(user);
        }
    }
}
