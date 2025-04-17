using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Identity;

namespace Talabat.Repository.Identity
{
	public static class AppUserSeed
	{
		public static async Task UserAsyncSeed(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any()) {
			var user = new AppUser()
			{
				DisplayName = "Abdelrahman Mohamed",
				Email = "abdelrahmanmohamed.route@gmail.com",
				UserName = "abdelrahmanmohamed",
				PhoneNumber = "01018734583"
			};
			await userManager.CreateAsync(user , "Pa$$w0rd");
			
			}
		}
	}
}
