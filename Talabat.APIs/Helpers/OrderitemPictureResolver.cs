using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.DTOs;
using Talabat.Core.Models.Order;

namespace Talabat.APIs.Helpers
{
	public class OrderitemPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderitemPictureResolver(IConfiguration configuration)
		{
			this._configuration = configuration;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.productItemOrdered.PictureUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}{source.productItemOrdered.PictureUrl}";
			}
			return string.Empty ;
		}
	}
}
