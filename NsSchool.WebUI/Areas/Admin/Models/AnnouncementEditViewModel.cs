using System.ComponentModel.DataAnnotations;

namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class AnnouncementEditViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="Bu alanı doldurmak zorunludur")]
		[Display(Name ="Başlık")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur")]
		[Display(Name = "Açıklama")]
		public string Discription { get; set; }

		[Display(Name = "Dosya Seç")]
		public IFormFile? Path { get; set; }
	}
}
