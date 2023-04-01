using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class AnnouncementEntity :BaseEntity //Duyuru
	{
		public string Title { get; set; }
		public string Discription { get; set; }
		public string? Path { get; set; }
	}

	public class AnnouncementEntityConfiguration : BaseConfiguration<AnnouncementEntity>
	{
		public override void Configure(EntityTypeBuilder<AnnouncementEntity> builder)
		{

           
            builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(60);

			builder.Property(x => x.Discription)
				.IsRequired()
				.HasMaxLength(300);

			builder.Property(x => x.Path)
				.IsRequired(false);


			base.Configure(builder);
		}
	}
}
