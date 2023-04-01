using NsSchool.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class ClassRoomTeacherListDto
	{
		public int ClassRoomId { get; set; }
		public int TeacherId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Branch { get; set; }
	}
}
