using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<string> GetTokenAsync(AppUser user)
		{
			//Payload
			var AuthClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName , user.DisplayName),
				new Claim(ClaimTypes.Email , user.Email)
			};
			///add claims to high security 
			// call key to create token but must be bytes
			var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"]));
			// token creation assign values from configratuion to jwt members
			var Token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIsser"], 
				audience: _configuration["JWT:ValidAudince"], 
				expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
				claims:AuthClaims,
				signingCredentials : new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
				);
			return  new JwtSecurityTokenHandler().WriteToken(Token);
		}
	}
}
