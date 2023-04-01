using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
	public interface IStudentService
	{
		ServiceMessage AddStudent(StudentParentDto studentParentDto);
		List<StudentListDto> GetAll();
		StudentDetailDto GetById(int id);
		StudentEditDto GetByStudent(int id);
		ServiceMessage EditStudent(StudentEditDto studentEditDto);
		void DeleteStudent(int id);
	}
}
