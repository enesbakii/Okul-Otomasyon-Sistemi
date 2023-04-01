using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class StudentEditViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Adı")]
		public string StundetFirstName { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Soyadı")]
		public string StudentLastName { get; set; }


		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Kullanıcı Adı")]
		public string StudentUserName { get; set; }


		[Display(Name = "Şifre")]
		public string? StudentPassword { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Kimlik No")]
		[StringLength(11, ErrorMessage = "Kimlik Numaranızı Kontrol Ediniz", MinimumLength = 11)]
		public string StudentIdentityPerson { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Öğrenci No")]
		public string StudentNumber { get; set; }


		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Sınıfı")]
		public string ClassRoom { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Kademe")]
		public string? StudentGrade
		{
			get { return "Anadolu Lisesi"; }
		}

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Alanı")]
		public string AreaInformation { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Doğum Tarihi")]
		public DateTime StundetBirthDay { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Cinsiyeti")]
		public bool StudentGender { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Telefon Numarası")]
		[StringLength(11, ErrorMessage = "Kimlik Numaranızı Kontrol Ediniz", MinimumLength = 11)]
		public string StudentPhoneNumber { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Adress")]
		public string StudentAdress { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Doğum Yeri")]
		public string StudentBirthPlace { get; set; }


		[Display(Name = "Resim")]
		public IFormFile? StudentImagePath { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Adı")]
		public string ParentFirstName { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Soyadı")]
		public string ParentLastName { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Kimlik No")]
		[StringLength(11, ErrorMessage = "Kimlik Numaranızı Kontrol Ediniz", MinimumLength = 11)]
		public string ParentIdentity { get; set; }


		[Display(Name = "Meslek")]
		public string? ParentJob { get; set; }


		[Display(Name = "Email")]
		public string? ParentEmail { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Doğum Tarihi")]
		public DateTime ParentBirthDay { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Cinsiyet")]
		public bool ParentGender { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Telefon Numarası")]
		[StringLength(11, ErrorMessage = "Kimlik Numaranızı Kontrol Ediniz", MinimumLength = 11)]
		public string ParentPhoneNumber { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Adres")]
		public string ParentAdress { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Doğum Yeri")]
		public string ParentBirthPlace { get; set; }



		[Required(ErrorMessage = "Bu alanı doldurmnak zorunludur")]
		[Display(Name = "Kullanıcı Adı")]
		public string ParentUserName { get; set; }

		[Display(Name = "Şifre")]
		public string? ParentPassword { get; set; }

		public int ParentId { get; set; }
	}
}
