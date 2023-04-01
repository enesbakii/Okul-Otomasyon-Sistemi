namespace NsSchool.WebUI.Areas.Teacher.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discripton { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
