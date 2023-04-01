using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class CategoryEntity :BaseEntity //Kategori
	{ 
		public string Name { get; set; }

		//relational property

		public List<ProductEntity> Products { get; set; }
	}

	public class CategoryEntityConfiguration : BaseConfiguration<CategoryEntity>
	{
		public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
		{
           
            builder.Property(x => x.Name)
				.IsRequired();

			base.Configure(builder);
		}
	}
}
