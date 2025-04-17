using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer Redis)
		{
			_database = Redis.GetDatabase();
		}

		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);	
		}

		public async Task<CustomerBasket?> GetBasketAsync(string basketId)
		{
			var basket = await _database.StringGetAsync(basketId);

			return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateOrCreateBasketAsync(CustomerBasket basket)
		{
			var serializedBasket = JsonSerializer.Serialize(basket);
			var createdOrUpdated = await _database.StringSetAsync(basket.Id, serializedBasket, TimeSpan.FromDays(1));
			if(!createdOrUpdated)return null; 
			return await GetBasketAsync(basket.Id);
		}
	}
}
