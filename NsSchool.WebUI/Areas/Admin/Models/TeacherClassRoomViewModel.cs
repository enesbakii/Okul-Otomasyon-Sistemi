namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class TeacherClassRoomViewModel
	{
		public int Id { get; set; }
		public int ClassRoomId { get; set; }
		public string ClassRoom { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
	}
}
