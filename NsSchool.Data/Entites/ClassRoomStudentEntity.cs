using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class ClassRoomStudentEntity :BaseEntity
	{
		public int StudentId { get; set; }
		public int ClassRoomId { get; set; }

		//relational property

		public StudentEntity Student { get; set; }
		public ClassRoomEntity ClassRoom { get; set; }
	}
	

}
