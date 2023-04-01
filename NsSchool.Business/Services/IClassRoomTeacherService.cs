using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
	public interface IClassRoomTeacherService
	{
		ServiceMessage AddClassRoomTeacher(List<ClassRoomTeacherDto> classRoomTeacherDtos);
		List<ClassRoomTeacherListDto> ClassRoomTeacherList(int id);
		void DeleteTeacher(ClassRoomTeacherDto classRoomTeacherDto);

        List<ClassRoomTeacherListDto> ListTeacher(int id);

		List<StudentsClassRoomsDto> ListStudent(int id);
    }
}
