using System.ComponentModel.DataAnnotations;

namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class AnnouncementFormViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Bu alan boş geçilemez.")]
		[Display(Name ="Başlık")]
		public string Title { get; set; }
		[Required(ErrorMessage ="Bu alan boş geçilemez")]
		[Display(Name ="Açıklama")]
		public string Discription { get; set; }
		[Display(Name ="Dosya")]
		public IFormFile? Path { get; set; }
	}
}
