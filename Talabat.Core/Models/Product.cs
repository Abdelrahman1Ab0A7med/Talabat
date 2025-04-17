using System.ComponentModel.DataAnnotations;

namespace Talabat.Core.Models
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }

		public ProductBrand ProductBrand { get; set; }
		public int ProductBrandId { get; set; }

		public ProductType ProductType { get; set; }
		public int ProductTypeId { get; set; }
	}
}
//DTOs