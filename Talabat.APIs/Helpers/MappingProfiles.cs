using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.APIs.DTOs.BasketDto;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Models.Order;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToDTO>().
				ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name)).
				ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name)).
				ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductImageResoulver>());
			
			CreateMap<Talabat.Core.Models.Identity.Address, AddressDto>().ReverseMap();
			CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
			CreateMap<Talabat.Core.Models.Order.Address,AddressDto>().ReverseMap();
			CreateMap<Order, OrderToReturnDto>().
				ForMember(d => d.DeliveryMethod, s => s.MapFrom(o => o.DeleiveryMethod.ShortName)).
				ForMember(d => d.DeliveryMethod, s => s.MapFrom(o => o.DeleiveryMethod.Cost));
			CreateMap<OrderItem, OrderItemDto>().
				ForMember(d=>d.ProductId,s=>s.MapFrom(o=>o.productItemOrdered.ProductId)).
				ForMember(d=>d.ProductName,s=>s.MapFrom(o=>o.productItemOrdered.ProductName)).
				ForMember(d=>d.PictureUrl,s=>s.MapFrom(o=>o.productItemOrdered.PictureUrl)).
				ForMember(d=>d.PictureUrl,s=>s.MapFrom<OrderitemPictureResolver>());

		}
	}
}
