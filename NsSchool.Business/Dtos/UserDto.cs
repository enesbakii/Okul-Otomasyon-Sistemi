using NsSchool.Data.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class UserDto
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string IdentityPerson { get; set; }

		public string ImagePath { get; set; }
		public UserTypeEnum UserType { get; set; }
		
	}
}
