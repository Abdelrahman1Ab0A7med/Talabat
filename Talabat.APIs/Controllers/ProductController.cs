using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Models;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
	public class ProductController : APIBaseController
	{
		private readonly IGenericRepository<Product> _product;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<ProductBrand> _productBrand;
		private readonly IGenericRepository<ProductType> _productType;

		public ProductController(IGenericRepository<Product> product, IMapper mapper, IGenericRepository<ProductBrand> productBrand, IGenericRepository<ProductType> productType)
		{
			_product = product;
			_mapper = mapper;
			_productBrand = productBrand;
			_productType = productType;
		}
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToDTO>>> GetProducts([FromQuery] ProductSpecParams Params)
		{
			var spec = new ProductSpecificationWithTypeAndBrand(Params);
			var products = await _product.GetAllWithSpecAsync(spec);

			var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToDTO>>(products);
			var CountSpec = new ProductCountSpec(Params);
			var Count = await _product.GetCountSpec(CountSpec);
			return Ok(new Pagination<ProductToDTO>(Params.PageSize,Params.pageIndex,Count,mappedProducts));
		}
		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetById(int id)
		{
			var spec = new ProductSpecificationWithTypeAndBrand(id);
			var product = await _product.GetByIdWithSpecAsync(spec);
			if(product == null) 
				return NotFound(new ApiResponse(404));
			var mappedProduct = _mapper.Map<Product, ProductToDTO>(product);
			return Ok(mappedProduct);
		}
		[HttpGet("Brands")]
		public async Task<ActionResult<ProductBrand>> GetALLProductBrands()
		{
			var Brands = await _productBrand.GetAll();
			return Ok(Brands);
		}
		[HttpGet("Types")]
		public async Task<ActionResult<ProductType>> GetALLProductTypes()
		{
			var Types = await _productType.GetAll();
			return Ok(Types);
		}
	}
}
