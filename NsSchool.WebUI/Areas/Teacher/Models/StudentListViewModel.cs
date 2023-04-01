namespace NsSchool.WebUI.Areas.Teacher.Models
{
    public class StudentListViewModel
    {
        public int Id { get; set; }
        public string StudentIdentity { get; set; }
        public string StdentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentUserName { get; set; }
        public string StudentPassword { get; set; }
        public string ImagePath { get; set; }
        public string ClassRoom { get; set; }
        public string ParentIdentity { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string StundetFullName { get { return StdentFirstName + " " + StudentLastName; } }
        public string ParentFullName { get { return ParentFirstName + " " + ParentLastName; } }
    }
}
