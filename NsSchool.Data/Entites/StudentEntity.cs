using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class StudentEntity : BasePersonEntity //Öğrenciler
	{
		public string StudentNumber { get; set; }
		public string ClassRoom { get; set; }
		public string Grade { get; set; }//kademe
		public string AreaInformation { get; set; }//alan bilgisi
		public int UserId { get; set; }

		public int ParentId { get; set; }


		//[ForeignKey(nameof(ParentEntity))]
		//public string ParentIdentityPerson { get; set; }


		//relational property
		public UserEntity User { get; set; }
		public ParentEntity Parent { get; set; }
		public List<ClassRoomStudentEntity> ClassRoomStudents { get; set; }

	}
	public class StudenEntityConfiguration : BaseConfiguration<StudentEntity>
	{
		public override void Configure(EntityTypeBuilder<StudentEntity> builder)
		{
           
            builder.Property(x => x.StudentNumber)
				.IsRequired()
				.HasMaxLength(8);

			builder.Property(x => x.ClassRoom)
				.IsRequired();

			builder.Property(x => x.Grade)
				.IsRequired();

			builder.Property(x => x.AreaInformation)
				.IsRequired();
         



            base.Configure(builder);
		}
		
	}
}
