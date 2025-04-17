using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Repositories
{
	public interface IBasketRepository
	{
		public Task<CustomerBasket?> GetBasketAsync(string basketId);
		public Task<CustomerBasket?> UpdateOrCreateBasketAsync(CustomerBasket basket);
		public Task<bool> DeleteBasketAsync(string basketId);

	}
}
