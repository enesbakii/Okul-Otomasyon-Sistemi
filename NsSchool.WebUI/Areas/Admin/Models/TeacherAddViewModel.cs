using NsSchool.Data.Enums;
using NsSchool.WebUI.Areas.Enums;
using System.ComponentModel.DataAnnotations;

namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class TeacherAddViewModel
	{

		[Required(ErrorMessage ="Bu alanı doldurmak zorunludur.")]
		[Display(Name ="Adı")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Soyadı")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Kimlik No")]
		[StringLength(11,ErrorMessage ="Bu alanı doldurmak zorunludur.",MinimumLength =11)]
		public string IdentityPerson { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Doğum Tarihi")]
		public DateTime BirthDay { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Cinsiyet")]
		public bool Gender { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Telefon No")]
		[StringLength(11, ErrorMessage = "Bu alanı doldurmak zorunludur.", MinimumLength = 11)]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Adres")]
		public string Adress { get; set; }

		[Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
		[Display(Name = "Doğum Yeri")]
		public string BirthPlace { get; set; }

		
		[Display(Name = "Resim")]
		public IFormFile? ImagePath { get; set; }

	
		[Display(Name = "Email")]
		public string? Email { get; set; }

		[Display(Name = "Branş")]
		[EnumDataType(typeof(BranchTypeEnum))]
		public BranchTypeEnum? Branch { get; set; }
	}
}
