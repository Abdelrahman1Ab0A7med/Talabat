using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order;

namespace Talabat.Repository.Data.Configurations.Order_Config
{
	public class DeleiveryMethodConfigurations : IEntityTypeConfiguration<DeleiveryMethod>
	{
		public void Configure(EntityTypeBuilder<DeleiveryMethod> builder)
		{
			builder.Property(o => o.Cost).HasColumnType("decimal(18,2)");
		}
	}
}
