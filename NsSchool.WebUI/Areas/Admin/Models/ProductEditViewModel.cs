using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class ProductEditViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Ad")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Açıklama")]
		public string Discription { get; set; }

		[Display(Name = "Resim Ekle")]
		public IFormFile? ImagePath { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Kategori")]
		public int CategoryId { get; set; }
		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Fiyat")]
		public decimal UnitPrice { get; set; }
	}
}

