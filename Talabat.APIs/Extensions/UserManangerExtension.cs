using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Models.Identity;

namespace Talabat.APIs.Extensions
{
	public static class UserManangerExtension
	{
		public static async Task<AppUser?> GetAddressUserAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.Users.Include(u => u.address).FirstOrDefaultAsync(u => u.Email == email);
			return user;
		}

	}
}
