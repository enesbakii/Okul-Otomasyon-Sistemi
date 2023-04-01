namespace NsSchool.WebUI.Areas.Student.Models
{
    public class ClassRoomStudentViewModel
    {
        public int ClassRoomId { get; set; }
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClassRoom { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
