using NsSchool.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
	public interface IUserService
	{
		UserDto LoginPerson(LoginDto loginDto);
		UserEditDto GetUser(int id);
		void EditUser(UserEditDto personEditDto);
		void EditPassword(PasswordEditDto passwordEditDto);


	}
}
