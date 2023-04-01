using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NsSchool.Business.Dtos
{
	public class PersonEditDto
	{
		public int Id { get; set; }
		public string FistName { get; set; }	
		public string LastName { get; set; }
		public string UserName { get; set; }

		public string? Password { get; set; }
		public string IdentityPerson { get; set; }	
		public string? Email { get; set; }	
		public string PhoneNumber { get; set; }	
		public bool Gender { get; set; }	
		public string Adress { get; set; }	
		public DateTime BirthDay { get; set; }		
		public string BirthPlace { get; set; }		
		public string? ImagePath { get; set; }
	}
}
