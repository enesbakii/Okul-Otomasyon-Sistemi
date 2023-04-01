using NsSchool.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class UserEditDto
	{
		public int Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? IdentityNumber { get; set; }
		public string? Email { get; set; }
		public DateTime? BirthDay { get; set; }
		public string Adress { get; set; }
		public string? PhoneNumber { get; set; }
		public string? ImagePath { get; set; }
		public int PersonId { get; set; }
	}
}
