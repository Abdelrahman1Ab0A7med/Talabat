namespace Talabat.Core.Models.Order
{
	public class   Order : BaseEntity
	{
		public Order()
		{
		}

		public Order(string buyerEmail, Address shippingAddress, DeleiveryMethod deleiveryMethod, ICollection<OrderItem> items, decimal subtoltal)
		{
			BuyerEmail = buyerEmail;
			ShippingAddress = shippingAddress;
			DeleiveryMethod = deleiveryMethod;
			Items = items;
			Subtoltal = subtoltal;
		}

		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
		public Address ShippingAddress { get; set; }
		public DeleiveryMethod DeleiveryMethod { get; set; }
		public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
		public decimal Subtoltal { get; set; }
		public string PaymentIntentId { get; set; } = string.Empty;
		public decimal Total()
		=> Subtoltal + DeleiveryMethod.Cost;
	}
}
