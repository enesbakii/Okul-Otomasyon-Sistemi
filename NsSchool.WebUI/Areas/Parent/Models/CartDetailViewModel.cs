using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace NsSchool.WebUI.Areas.Parent.Models
{
	public class CartDetailViewModel
	{
		public int ProductId { get; set; }
		public int UserId { get; set; }
		public int Count { get; set; }
		[Required(ErrorMessage ="Adet Boş Geçilemez")]
		public decimal UnitPrice { get; set; }
	}
}
