using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
	public class RegisterDto
	{
		[Required]
		[EmailAddress]

		public string Email { get; set; }
		[Required]
		public string DisplayName { get; set; }
		[Required]
		[Phone]
		public string PhoneNumber { get; set; }
		[Required]
		[RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
							ErrorMessage = "Your password must contain 1 digit ,1 number ,1 lower case ,1 upper case ,1 Special Character")]
		public string Password { get; set; }


	}
}