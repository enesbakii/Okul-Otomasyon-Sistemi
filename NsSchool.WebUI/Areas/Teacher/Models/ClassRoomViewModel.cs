using NsSchool.Business.Dtos;

namespace NsSchool.WebUI.Areas.Teacher.Models
{
	public class ClassRoomViewModel
	{
		public ClassRoomStudentListDto Students { get; set; }
		public string ClassRoom { get; set; }
	}
}
