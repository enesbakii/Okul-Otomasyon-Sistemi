namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class StudentClassRoomViewModel
	{
		public int ClassRoomId { get; set; }
		public int StudentId { get; set; }
		public string ClassRoomName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
	}
}
