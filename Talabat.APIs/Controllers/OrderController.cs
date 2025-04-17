using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Models.Order;
using Talabat.Core.Repositories;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
	
	public class OrderController : APIBaseController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IUnitOfWork unitOfWork,IOrderService orderService,IMapper mapper)
		{
			this._unitOfWork = unitOfWork;
			this._orderService = orderService;
			this._mapper = mapper;
		}
		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[Authorize]
		[HttpPost]
		public async Task<ActionResult<Order>> GetOrders(OrderDto order)
		{
			var email =  User.FindFirstValue(ClaimTypes.Email);
			var mappedAddress = _mapper.Map<AddressDto,Address>(order.ShippingAddress);
			var Order = await _orderService.CreateOredrAsync(email, order.BasketId, order.DeliveryMethodId, mappedAddress);
			if (Order is null) return BadRequest(new ApiResponse(400,message:"There is an issue in your order"));
			return Ok(Order);

		}
		[ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[Authorize]
		[HttpGet("GetUserOrders")]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetUserOrders()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderService.GetOrdersForSpecificUser(email);
			var mappedOrder = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(order);
			if (mappedOrder is null) return NotFound(new ApiResponse(404));
			return Ok(mappedOrder);
		}
		[ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderToReturnDto>> GetSpecificOrderforSpecificUser(int id)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderService.GetOrderByIdForSpecificUser(email, id);
			var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);	
			if (mappedOrder is null) return NotFound(new ApiResponse(404,message:$"There is no order with this id : {id}"));
			return Ok(mappedOrder);
		}
		[Authorize]
		[HttpGet("DeliveryMethod")]
		public async Task<ActionResult<IReadOnlyList<DeleiveryMethod>>> GetDeliveryMethods()
		{
			var deleveryMethods = await _unitOfWork.Repository<DeleiveryMethod>().GetAll();
			if (deleveryMethods is null) return NotFound(new ApiResponse(404));
			return Ok(deleveryMethods);
		}

	}
}
