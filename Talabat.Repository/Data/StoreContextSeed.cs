using System.Text.Json;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext context)
		{
			if (!context.ProductBrands.Any())
			{
				var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
				//Read all content and store it in string
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
				//convert string to array of objects
				if (brands?.Count > 0)
				{
					foreach (var brand in brands)
					{
						await context.Set<ProductBrand>().AddAsync(brand);
					}
					await context.SaveChangesAsync();
				}
			}
			if (!context.ProductTypes.Any())
			{
				var typesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
				if (types?.Count > 0)
				{
					foreach (var type in types)
					{
						await context.Set<ProductType>().AddAsync(type);
					}
					await context.SaveChangesAsync();
				}
			}
			if (!context.Products.Any())
			{
				var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				//Read all content and store it in string
				var Products = JsonSerializer.Deserialize<List<Product>>(productData);
				//convert string to array of objects
				if (Products?.Count > 0)
				{
					foreach (var product in Products)
					{
						await context.Set<Product>().AddAsync(product);
					}
					await context.SaveChangesAsync();
				}
			}
			if (!context.DeleiveryMethods.Any())
			{
				var Deleveries = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
				//Read all content and store it in string
				var Methods = JsonSerializer.Deserialize<List<DeleiveryMethod>>(Deleveries);
				//convert string to array of objects
				if (Methods?.Count > 0)
				{
					foreach (var method in Methods)
					{
						await context.Set<DeleiveryMethod>().AddAsync(method);
					}
					await context.SaveChangesAsync();
				}
			}
		}

	}
}
