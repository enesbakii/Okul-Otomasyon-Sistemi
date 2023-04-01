using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
	public interface IClassRoomStudentService
	{
		ServiceMessage AddStudent(List<ClassRoomStudentDto> classRoomStudentDtos);
		List<ClassRoomStudentListDto> ClassRoomStudentList(int id);
		void DeleteStudent(ClassRoomStudentDto classRoomStudentDto);

        List<ClassRoomStudentListDto> ListStudent(int id);
	}
}
