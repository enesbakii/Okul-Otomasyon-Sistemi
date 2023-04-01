using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NsSchool.Business.Dtos
{
	public class StudentParentDto
	{
		public string StundetFirstName { get; set; }
		public string StudentLastName { get; set; }
		public string StudentIdentityPerson { get; set; }
		public string StudentNumber { get; set; }
		public string ClassRoom { get; set; }
		public string StudentGrade { get; set; }
		public string AreaInformation { get; set; }
		public DateTime StundetBirthDay { get; set; }
		public bool StudentGender { get; set; }
		public string StudentPhoneNumber { get; set; }
		public string StudentAdress { get; set; }
		public string StudentBirthPlace { get; set; }
		public string? StudentImagePath { get; set; }
		public string ParentFirstName { get; set; }
		public string ParentLastName { get; set; }
		public string ParentIdentity { get; set; }
		public string? ParentJob { get; set; }
		public string? ParentEmail { get; set; }
		public DateTime ParentBirthDay { get; set; }
		public bool ParentGender { get; set; }
		public string ParentPhoneNumber { get; set; }
		public string ParentAdress { get; set; }
		public string ParentBirthPlace { get; set; }

        public int ParentId { get; set; }
    }
}
