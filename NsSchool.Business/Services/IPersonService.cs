using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
	public interface IPersonService
	{
		

		List<PersonListDto> GetPersonList();

		ServiceMessage AddPerson(PersonAddDto personAddDto);

		PersonEditDto GetPerson(int id);

		ServiceMessage EditPerson(PersonEditDto personEditDto);

		void DeletePerson(int id);
	}
}
