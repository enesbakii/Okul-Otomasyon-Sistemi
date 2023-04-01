namespace NsSchool.WebUI.Areas.Parent.Models
{
	public class StudentListViewModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
	}
}
