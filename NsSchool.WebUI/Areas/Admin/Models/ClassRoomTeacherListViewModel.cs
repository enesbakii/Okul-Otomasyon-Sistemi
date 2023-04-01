namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class ClassRoomTeacherListViewModel
	{
		public int ClassRoomId { get; set; }
		public int TeacherId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Branch { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
	}
}
