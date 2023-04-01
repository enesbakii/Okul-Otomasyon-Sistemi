using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NsSchool.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public abstract class BasePersonEntity:BaseEntity //Personeller
	{

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string IdentityPerson { get; set; }
		public DateTime BirthDay { get; set; }
		public bool Gender { get; set; }
		public string PhoneNumber { get; set; }
		public string Adress { get; set; }
		public UserTypeEnum UserType { get; set; }
		public string BirthPlace { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string? ImagePath { get; set; }
	}

	public abstract class BasePersonEntityConfiguration : BaseConfiguration<BasePersonEntity> {
		public override void Configure(EntityTypeBuilder<BasePersonEntity> builder)
		{
			builder.Property(x => x.FirstName)
				.IsRequired()
				.HasMaxLength(40);

			builder.Property(x => x.LastName)
				.IsRequired()
				.HasMaxLength(40);

			builder.Property(x => x.IdentityPerson)
				.IsRequired()
				.HasMaxLength(11);

			builder.Property(x => x.Gender)
				.IsRequired();

			builder.Property(x => x.PhoneNumber)
				.IsRequired()
				.HasMaxLength(11);

			builder.Property(x=>x.Adress)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.BirthDay)
				.IsRequired();

			builder.Property(x => x.UserName)
				.IsRequired()
				.HasMaxLength(60);

			builder.Property(x => x.Password)
				.IsRequired();

			builder.Property(x=>x.ImagePath)
				.IsRequired(false);

			base.Configure(builder);
		}
	}
}
