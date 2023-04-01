using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class ParentEntity :BasePersonEntity //Veli
	{
		public string? Job { get; set; }
		public string? Email { get; set; }

		public int UserId { get; set; }

		//relational property
		public UserEntity User { get; set; }
		public List<StudentEntity> Students { get; set; }
	}

	public class ParentEntityConfiguration: BaseConfiguration<ParentEntity>
	{
		public override void Configure(EntityTypeBuilder<ParentEntity> builder)
		{
			//builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(x => x.Job).IsRequired(false);

			builder.Property(x=>x.Email).IsRequired(false);

			base.Configure(builder);
		}
	}
}
