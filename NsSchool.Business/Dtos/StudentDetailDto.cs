using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class StudentDetailDto
	{
		public int Id { get; set; }
		public string StundetFirstName { get; set; }
		public string StudentLastName { get; set; }
		public string StudentIdentityPerson { get; set; }
		public string StudentNumber { get; set; }
		public string ClassRoom { get; set; }
		public string StudentGrade { get; set; }
		public string AreaInformation { get; set; }
		public DateTime StundetBirthDay { get; set; }
		
		public string StudentPhoneNumber { get; set; }
		public string StudentAdress { get; set; }
		public string StudentBirthPlace { get; set; }
		
		public string ParentFirstName { get; set; }
		public string ParentLastName { get; set; }
		public string ParentIdentity { get; set; }
		public string? ParentJob { get; set; }
		public string? ParentEmail { get; set; }
		public DateTime ParentBirthDay { get; set; }
		
		public string ParentPhoneNumber { get; set; }
		public string ParentAdress { get; set; }
		public string ParentBirthPlace { get; set; }

		public string StudentUserName { get; set; }
		public string StudentPassword { get; set; }
		public string ParentUserName { get; set; }
		public string ParentPassword { get; set; }
	}
}
