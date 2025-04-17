
using Microsoft.AspNetCore.Identity;

namespace Talabat.APIs.Errors
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public IEnumerable<IdentityError> Errors { get; set; }
		public ApiResponse(int statusCode, string? message = null, IEnumerable<IdentityError>? errors = null) {
			StatusCode = statusCode;
			Errors = errors;
			Message = message ?? getMessageForStatusCode(statusCode);
		}

		

		private string? getMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "Bad Request",
				401 => "unAuthorized",
				404 => "Resource Not Found",
				500 => "Internal server error ",
				_ => null
			};
		}
	}
}
