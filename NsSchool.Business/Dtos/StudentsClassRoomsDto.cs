using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class StudentsClassRoomsDto
	{
		public ClassRoomStudentListDto Students { get; set; }
		public string ClassRoom { get; set; }

	}
}
