using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
	public class DummyController : APIBaseController
	{
		private readonly StoreContext _dbContext;

		public DummyController(StoreContext DbContext) {
			_dbContext = DbContext;
		}
		[HttpGet("NotFound")]
		public ActionResult GetNotFoundRequest() {
			var product = _dbContext.Products.Find(100);
			if (product == null)  return NotFound(new ApiResponse(404));
			return Ok(product);
		}

		[HttpGet("ServerError")]
		public ActionResult GetServerErrorRequest()
		{
			var product = _dbContext.Products.Find(100);
			var f = product.ToString();//error
			return Ok(f);
		}

		[HttpGet("BadRequest/{id}")]
		public ActionResult GetBadRequestByid(int id)
		{
			
			return Ok();
		}

		[HttpGet("BadRequest")]
		public ActionResult GetBadRequest()
		{

			return BadRequest(new ApiResponse(400));
		}
	}
}
