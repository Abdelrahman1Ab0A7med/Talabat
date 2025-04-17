using Talabat.Core.Models.Order;

namespace Talabat.APIs.DTOs
{
	public class OrderToReturnDto
	{
		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public string OrderStatus { get; set; } 
		public Address ShippingAddress { get; set; }
		public string DeliveryMethod { get; set; }
		public decimal DeliveryMethodCost { get; set; }
		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
		public decimal Subtoltal { get; set; }
		public decimal Total { get; set; }
		public string PaymentIntentId { get; set; } 
	}
}
