using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
	public static class IdentityServices
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddAuthorization(options =>
			{
				options.DefaultPolicy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
					.Build();
			});

			services.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			})
					.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["JWT:ValidIsser"],
					ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAudince"],
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"]))
				};
			});

			services.AddScoped(typeof(ITokenService) , typeof(TokenService));

			services.AddIdentity<AppUser, IdentityRole>()
					.AddEntityFrameworkStores<UserContext>();

			return services;
		}
	}
}
