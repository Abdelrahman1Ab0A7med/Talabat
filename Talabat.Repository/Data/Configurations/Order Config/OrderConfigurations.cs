using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order;

namespace Talabat.Repository.Data.Configurations
{
	public class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(o=>o.OrderStatus).HasConversion(ostatus => ostatus.ToString(),ostatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),ostatus) );

			builder.OwnsOne(o => o.ShippingAddress, sa => sa.WithOwner());
			builder.HasOne(o=>o.DeleiveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
			builder.Property(o=>o.Subtoltal).HasColumnType("decimal(18,2)");

		}
	}
}
