using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
	//filteration object
	public class ProductSpecParams //: Specification<Product>
	{
		public int? brandId { get; set; }
		public int? typeId { get; set; }
		public string? sort { get; set; }
		public int pageIndex { get; set; } = 1;
		private int pageSize = 5;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > 10 ? 10 : value; }
		}
		private string? search;

		public string? Search
		{
			get { return search; }
			set { search = value?.ToLower(); }
		}

	}
}
