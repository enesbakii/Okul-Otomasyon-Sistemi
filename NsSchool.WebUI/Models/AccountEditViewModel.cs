using System.ComponentModel.DataAnnotations;

namespace NsSchool.WebUI.Models
{
	public class AccountEditViewModel
	{
		public int Id { get; set; }
		[Display(Name ="Ad:")]
		
		public string? FirstName { get; set; }

		[Display(Name = "Soyad:")]
		
		public string? LastName { get; set; }

		[Display(Name = "Kimlik No:")]
		
		public string? IdentityNumber { get; set; }

		[Display(Name = "Mail Adresi:")]
		public string? Email { get; set; }

		[Display(Name = "Doğum Tarihi:")]
		//[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
		public DateTime? BirthDay { get; set; }

		[Display(Name = "Adres:")]
		public string Adress { get; set; }
		[Display(Name = "Telefon Numarası:")]
		public string? PhoneNumber { get; set; }

		[Display(Name = "Resim Seç:")]
		public IFormFile? ImagePath { get; set; }
	}
}
