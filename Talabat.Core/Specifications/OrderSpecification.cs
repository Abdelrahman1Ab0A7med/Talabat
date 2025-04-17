using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order;

namespace Talabat.Core.Specifications
{
	public class OrderSpecification :Specification<Order>
	{
		public OrderSpecification(string email):base(o=>o.BuyerEmail==email)
		{
			AddIncludes();
			AddOrderByDesc(o=>o.OrderDate);
		}
		public OrderSpecification(int OrderId,string email):base(o=>o.Id == OrderId&&o.BuyerEmail ==email)
		{
			AddIncludes();	
		}

		private void AddIncludes()
		{
			Includes.Add(o => o.DeleiveryMethod);
			Includes.Add(o => o.Items);
		}
	}
}
