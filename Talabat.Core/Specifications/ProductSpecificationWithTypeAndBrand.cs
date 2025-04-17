using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
	public class ProductSpecificationWithTypeAndBrand : Specification<Product>
	{
		public ProductSpecificationWithTypeAndBrand(ProductSpecParams Params) : base(
			p =>
			(string.IsNullOrEmpty(Params.Search) || p.Name.Contains(Params.Search))
			&&
			(!Params.brandId.HasValue || p.ProductBrandId == Params.brandId)
			&&
			(!Params.typeId.HasValue || p.ProductTypeId == Params.typeId)
			)
		{
			Includes.Add(p => p.ProductBrand);
			Includes.Add(p => p.ProductType);
			if (!string.IsNullOrEmpty(Params.sort))
			{
				switch (Params.sort)
				{
					case "PriceAsc":
						OrderBy = p => p.Price;
						break;
					case "PriceDesc":
						OrderByDesc = p => p.Price;
						break;
					default:
						OrderBy = p => p.Name;
						break;
				}
			}

			ApplyPagination(Params.PageSize * (Params.pageIndex - 1), Params.PageSize);
		}
		public ProductSpecificationWithTypeAndBrand(int id) : this(new ProductSpecParams())
		{
			Criteria = p => p.Id == id;
		}

	}
}
