using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =  true)]
	public class ErrorsController : ControllerBase
	{
		//for not found end point
		public ActionResult Error(int code)
		{
			return Unauthorized(new ApiResponse(code));
		}
	}
}
