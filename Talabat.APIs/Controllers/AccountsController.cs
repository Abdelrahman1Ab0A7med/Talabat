using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{

	public class AccountsController : APIBaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _token;
		private readonly IMapper _mapper;

		public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService,IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_token = tokenService;
			_mapper = mapper;
		}
		//Register
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			if(EmailCheckDublicate(model.Email).Result.Value)return BadRequest(new ApiResponse(400,message:"This Email has been registered before "));
			var User = new AppUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				PhoneNumber = model.PhoneNumber
			};
			var result = await _userManager.CreateAsync(User, model.Password);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400,errors:result.Errors));
			return new UserDto() { DisplayName = model.DisplayName, Email = model.Email, Token = await _token.GetTokenAsync(User) };
		}


		//Login
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null) return Unauthorized(new ApiResponse(401));
			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
			return new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token = await _token.GetTokenAsync(user) };

		}
		[Authorize]
		[HttpGet("GetCurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);
			return new UserDto { DisplayName = user.DisplayName, Email = user.Email, Token = await _token.GetTokenAsync(user) };
		}
		[Authorize]
		[HttpGet("GetCurrentUserAddress")]
		public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
		{
			var user = await _userManager.GetAddressUserAsync(User);
			var returnedAddress = _mapper.Map<Address,AddressDto>(user.address);
			return returnedAddress;
		}
		[Authorize]
		[HttpPut("Address")]
		public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto UpdatedAddress)
		{
			var user = await _userManager.GetAddressUserAsync(User);
			var mappedAddress = _mapper.Map<AddressDto,Address>(UpdatedAddress);
			user.address.Id = UpdatedAddress.Id;
			user.address = mappedAddress;
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400));
			return UpdatedAddress;
		}
		[Authorize]
		[HttpGet("Emailexists")]
		public async Task<ActionResult<bool>> EmailCheckDublicate(string Email)
		{
			return await _userManager.FindByEmailAsync(Email) is not null;
		}

	}
}
