using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.BasketDto
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string productName { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Type { get; set; }
		[Required]
		[Range(0.1,double.MaxValue,ErrorMessage ="Invalid Price")]
		public decimal Price { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Invalid Quantity")]
		public int Quantity { get; set; }
	}

}
