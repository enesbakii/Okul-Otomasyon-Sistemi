using System.ComponentModel.DataAnnotations;

namespace NsSchool.WebUI.Models
{
	public class PasswordEditViewModel
	{

		public int Id { get; set; }

		[Required(ErrorMessage ="Bu alanı doldurmak zorunludur.")]
		[Display(Name ="Şifre:")]
		public string Password { get; set; }


		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Şifre Tekrar:")]
		[Compare(nameof(Password),ErrorMessage ="Şifreler eşleşmiyor")]
		public string PasswordConfirm { get; set; }
	}
}
