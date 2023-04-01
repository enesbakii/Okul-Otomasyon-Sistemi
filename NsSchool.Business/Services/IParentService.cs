using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NsSchool.Business.Services
{
	public interface IParentService
	{
		ServiceMessage AddParent(StudentParentDto studentParentDto);
		ServiceMessage EditParent(StudentEditDto parentEditDto);
		StudentParentDto GetByParent();
		StudentParentDto GetByParentIdentity(string parentIdentity);
		List<ClassRoomStudentListDto> GetStudents(int id);
	}
}
