using Talabat.Core.Models.Order;

namespace Talabat.Core.Services
{
	public interface IOrderService
	{
		Task<Order> CreateOredrAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
		Task<IReadOnlyList<Order>> GetOrdersForSpecificUser(string BuyerEmail);
		Task<Order> GetOrderByIdForSpecificUser(string BuyerEmail,int OrderId);
	}
}
