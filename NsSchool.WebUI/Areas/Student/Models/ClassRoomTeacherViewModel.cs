namespace NsSchool.WebUI.Areas.Student.Models
{
    public class ClassRoomTeacherViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Branch { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
