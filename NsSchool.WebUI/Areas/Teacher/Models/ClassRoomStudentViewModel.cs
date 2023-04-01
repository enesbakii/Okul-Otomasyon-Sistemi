namespace NsSchool.WebUI.Areas.Teacher.Models
{
	public class ClassRoomStudentViewModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ClassRoom { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
	}
}
