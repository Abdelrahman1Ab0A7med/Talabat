namespace Talabat.Core.Models.Order
{
	public class OrderItem : BaseEntity
	{
		public OrderItem()
		{

		}
		public OrderItem(ProductItemOrdered productItemOrdered, decimal price, int quantity)
		{
			this.productItemOrdered = productItemOrdered;
			Price = price;
			Quantity = quantity;
		}

		public ProductItemOrdered productItemOrdered { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }

	}
}
