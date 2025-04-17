using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.BasketDto;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{
	
	public class BasketController : APIBaseController
	{
		private readonly IBasketRepository _basket;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basket, IMapper mapper)
		{
			_basket = basket;
			_mapper = mapper;
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
		{
			var basket = await _basket.GetBasketAsync(id);
			return basket is null ? new CustomerBasketDto(id) : Ok(basket);
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
		{ 
			var mappedBasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
			var CreatedOrUpdatedBasket  = await _basket.UpdateOrCreateBasketAsync(mappedBasket);
			if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
			return Ok(CreatedOrUpdatedBasket);
		}

		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
		{
			var DeletedBasket = await _basket.DeleteBasketAsync(BasketId);
			return DeletedBasket;
		}

	}
}
