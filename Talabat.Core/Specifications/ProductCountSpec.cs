using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
	public class ProductCountSpec : Specification<Product>
	{
		public ProductCountSpec(ProductSpecParams Params):base(
			p =>
			(string.IsNullOrEmpty(Params.Search) || p.Name.Contains(Params.Search))
			&&
			(!Params.brandId.HasValue|| p.ProductBrandId == Params.brandId) 
			&&
			(!Params.typeId.HasValue||p.ProductTypeId == Params.typeId)
			)
		{
			
		}
	}
}
