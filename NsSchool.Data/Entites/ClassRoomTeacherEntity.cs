using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class ClassRoomTeacherEntity :BaseEntity
	{
		public int ClassRoomId { get; set; }
		public int PersonId { get; set; }

		//relational property

		public ClassRoomEntity ClassRoom { get; set; }
		public  PersonEntity Person { get; set; }
	}
}
