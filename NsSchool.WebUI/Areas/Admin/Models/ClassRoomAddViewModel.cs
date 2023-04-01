using System.ComponentModel.DataAnnotations;

namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class ClassRoomAddViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Bu alanı doldurmak zorunludur")]
		[Display(Name="Sınıf Adı")]
		public string Name { get; set; }
	}
}
