using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class ProductEntity:BaseEntity //Ürünler
	{
		public string Name { get; set; }
		public string Discription { get; set; }
		public decimal Price { get; set; }
		public string ImagePath { get; set; }
		public int CategoryId { get; set; }

		

		//relational porperty

		public CategoryEntity Category { get; set; }
		public List<CartDetailEntity> CartDetails { get; set; }
	}

	public class ProductEntityConfiguration: BaseConfiguration<ProductEntity>
	{
		public override void Configure(EntityTypeBuilder<ProductEntity> builder)
		{
            
            builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(70);

			builder.Property(x => x.Discription)
				.IsRequired()
				.HasMaxLength(300);

			builder.Property(x => x.Price)
				.IsRequired();

			builder.Property (x => x.ImagePath)
				.IsRequired();

			builder.Property(x=>x.CategoryId)
				.IsRequired();



			base.Configure(builder);
		}
	}
}
