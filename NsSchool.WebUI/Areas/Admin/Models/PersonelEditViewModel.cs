using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class PersonelEditViewModel
	{
		public int Id { get; set; }

		[Display(Name ="Ad")]
		[Required(ErrorMessage ="Bu alanı doldurmak zorunludur.")]
		public string FirstName { get; set; }

		[Display(Name ="Soyad")]
		[Required(ErrorMessage ="Bu alanı doludrmak zorunludur.")]
		public string LastName { get; set; }


		[Display(Name = "Kullanıcı Adı")]
		[Required(ErrorMessage = "Bu alanı doludrmak zorunludur.")]
		public string UserName { get; set; }

		[Display(Name = "Şifre")]
		public string? Password { get; set; }

		[Display(Name = "Kimlik Numarası")]
		[Required(ErrorMessage = "Bu alanı doludrmak zorunludur.")]
		[StringLength(11, ErrorMessage = "Kimlik numarasını kontrol ediniz ", MinimumLength = 11)]
		public string IdentityPerson { get; set; }

		[Display(Name = "Email Adresi")]
		public string? Email { get; set; }

		[Display(Name = "Telefon Numarası")]
		[Required(ErrorMessage = "Bu alanı doludrmak zorunludur.")]
		[StringLength(11,ErrorMessage ="Telefon numarasını kontrol ediniz ",MinimumLength =11)]
		public string PhoneNumber { get; set; }

		[Display(Name ="Cinsiyet")]
		[Required(ErrorMessage ="Bu alanı doldurmak zorunludur")]
		public bool Gender { get; set; }

		[Display(Name = "Adres")]
		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur")]
		public string Adress { get; set; }

		[Display(Name = "Doğum Tarihi")]
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		public DateTime BirthDay { get; set; }

		[Display(Name ="Doğum Yeri")]
		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur")]
		public string BirthPlace { get; set; }

		[Display(Name ="Resim Seç")]
		public IFormFile? ImagePath { get; set; }



	}
}
