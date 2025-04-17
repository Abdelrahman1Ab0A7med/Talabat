using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Models;

namespace Talabat.APIs.Helpers
{
	public class ProductImageResoulver : IValueResolver<Product, ProductToDTO, string>
	{
		private readonly IConfiguration _configuration;

		public ProductImageResoulver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Product source, ProductToDTO destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl)) {
				return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
			}
			return string.Empty;
		}
	}
}
//create class inherit from IValueResolver give it your attributes 