using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
	public interface IClassRoomService
	{
		List<ClassRoomDto> GetClassRoomList();

		ServiceMessage AddClassRoom(ClassRoomDto classRoomDto);

		ClassRoomDto GetClassRoomById(int id);
		ServiceMessage UpdateClassRoom(ClassRoomDto classRoomDto);
		void DeleteClassRoom(int id);

		
	}
}
