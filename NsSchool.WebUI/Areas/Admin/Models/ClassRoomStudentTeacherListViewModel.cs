namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class ClassRoomStudentTeacherListViewModel
	{
		public List<ClassRoomTeacherListViewModel> TeacherClassRoomList { get; set; }
		public List<StudentClassRoomViewModel> StudentClassRoomList { get; set; }
	}
}
