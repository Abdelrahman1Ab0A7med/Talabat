using Talabat.Core.Models;

namespace Talabat.APIs.DTOs.BasketDto
{
	public class CustomerBasketDto
	{
		public CustomerBasketDto(string id)
		{
			Id = id;
		}

		public string Id { get; set; }
		public List<BasketItemDto> Items { get; set; }
	}
}
