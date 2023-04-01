using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class PersonEntity :BasePersonEntity //Personeller
	{
		public string? Email { get; set; }
		public string? Branch { get; set; }
		public int UserId { get; set; }



		//relational property
		public UserEntity User { get; set; }

		public List<ClassRoomTeacherEntity> ClassRoomTeachers { get; set; }

	}

	public class PersonEntityConfiguraiton: BaseConfiguration<PersonEntity>
	{
		public override void Configure(EntityTypeBuilder<PersonEntity> builder)
		{
        
            builder.Property(x => x.Email).IsRequired(false);

			builder.Property(x=>x.Branch).IsRequired(false);

          
		}
	}
}
