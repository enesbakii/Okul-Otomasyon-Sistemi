using System.ComponentModel.DataAnnotations;

namespace NsSchool.WebUI.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage ="Lütfen Kullanıcı Adını Giriniz")]
		[Display(Name ="Kullanıcı Adı")]
		public string UserName { get; set; }
		[Required(ErrorMessage ="Lütfen Şifrenizi Giriniz")]
		[Display(Name ="Şifre")]
		public string Password { get; set; }
	}
}
