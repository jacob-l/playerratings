using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PlayerRatings.Models;

namespace PlayerRatings.Util
{
    public static class ClaimsPrincipalExtensions
    {
        public static async Task<ApplicationUser> GetApplicationUser(this ClaimsPrincipal user, UserManager<ApplicationUser> userManager)
        {
            var userId = user.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return await userManager.FindByIdAsync(user.GetUserId());
        }
    }
}
