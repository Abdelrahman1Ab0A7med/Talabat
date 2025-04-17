using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.Service
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IBasketRepository basketRepo,IUnitOfWork unitOfWork)
		{
			this._basketRepo = basketRepo;
			this._unitOfWork = unitOfWork;
		}

		public async Task<Order> CreateOredrAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
		{
			//1.Get Basket From Basket Repo
			var Basket = await _basketRepo.GetBasketAsync(BasketId);
			//2.Get Selected Items at Basket From Product Repo
			List<OrderItem> orderItems = new List<OrderItem>();
			if (Basket?.Items.Count > 0)
			{
				foreach(var item in Basket.Items)
				{
					var product =  await  _unitOfWork.Repository<Product>().GetById(item.Id);
					var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
					var order = new OrderItem(productItemOrdered, product.Price, item.Quantity);
					orderItems.Add(order);
				}
			}
			//3.Calculate SubTotal
			var subTotal=orderItems.Sum(o=>o.Quantity *  o.Price);
			//4.Get Delivery Method From DeliveryMethod Repo
			var delieveryMethod = await _unitOfWork.Repository<DeleiveryMethod>().GetById(DeliveryMethodId);
			//5.Create Order
			var Order = new Order(BuyerEmail, ShippingAddress, delieveryMethod, orderItems,subTotal);
			//6.Add Order Locally
			await _unitOfWork.Repository<Order>().Add(Order);

			//7.Save Order To Database[ToDo]
			var Result =  await _unitOfWork.CompleteAsync();
			if (Result < 0)
				return null;
			return Order;
		}

		public Task<Order> GetOrderByIdForSpecificUser(string BuyerEmail, int OrderId)
		{
			var spec = new OrderSpecification(OrderId,BuyerEmail);
			var order = _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
			return order;
		}

		public Task<IReadOnlyList<Order>> GetOrdersForSpecificUser(string BuyerEmail)
		{
			var spec = new OrderSpecification(BuyerEmail);
			var order = _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
			return order;
		}
	}
}
