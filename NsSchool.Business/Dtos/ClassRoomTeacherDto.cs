using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class ClassRoomTeacherDto
	{
		public int ClassRoomId { get; set; }
		public int TeacherId { get; set; }
		public string Name { get; set; }
	}
}
