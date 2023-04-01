using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class ClassRoomEntity :BaseEntity //Sınıflar
	{
		public string Name { get; set; }


		//relational property

		public List<ClassRoomTeacherEntity> ClassRoomTeachers { get; set; }
		public List<ClassRoomStudentEntity> ClassRoomStudents { get; set; }
	}

	public class ClassRoomEntitiyConfiguration : BaseConfiguration<ClassRoomEntity>
	{
		public override void Configure(EntityTypeBuilder<ClassRoomEntity> builder)
		{
          
            builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(20);

			base.Configure(builder);
		}
	}
}
