using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.Data.Entites;
using NsSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Managers
{
	public class UserManager:IUserService
	{
		private readonly IRepository<UserEntity> _userRepository;
		private readonly IDataProtector _dataProtector;
		public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
		{
			_userRepository = userRepository;
			_dataProtector = dataProtectionProvider.CreateProtector("security");
		}

		public void EditPassword(PasswordEditDto passwordEditDto)
		{
			var entity = _userRepository.GetAll()
				.Include(x=>x.Person)
				.Include(x=>x.Student)
				.Include(x=>x.Parent)
				.FirstOrDefault(x=>x.Id==passwordEditDto.Id);
				



			var encryptedPassword = _dataProtector.Protect(passwordEditDto.Password);
			entity.Password = encryptedPassword;
			if (entity.UserType == Data.Enums.UserTypeEnum.Admin || entity.UserType == Data.Enums.UserTypeEnum.Teacher || entity.UserType == Data.Enums.UserTypeEnum.Person)
			{
				entity.Person.Password = encryptedPassword;
			}
			else if (entity.UserType == Data.Enums.UserTypeEnum.Student)
			{
				entity.Student.Password = encryptedPassword;
			}
			else if (entity.UserType == Data.Enums.UserTypeEnum.Parent)
			{
				entity.Parent.Password = encryptedPassword;
			}
			

			_userRepository.Update(entity);
		}

		public void EditUser(UserEditDto userEditDto)
		{
			var entityList = _userRepository.GetAll();
			var entity = entityList
				.Include(x=>x.Person)
				.Include(x => x.Student)
				.Include(x=>x.Parent)
				.FirstOrDefault(x=>x.Id==userEditDto.Id);
			if (entity.UserType==Data.Enums.UserTypeEnum.Admin || entity.UserType == Data.Enums.UserTypeEnum.Teacher || entity.UserType == Data.Enums.UserTypeEnum.Person)

			{
				entity.Person.BirthDay =Convert.ToDateTime(userEditDto.BirthDay);
				entity.Person.Adress = userEditDto.Adress;
				entity.Person.PhoneNumber= userEditDto.PhoneNumber;
				entity.Person.Email= userEditDto.Email;
				if (userEditDto.ImagePath!=null)
				{
					entity.Person.ImagePath = userEditDto.ImagePath;
				}
			}


			else if (entity.UserType == Data.Enums.UserTypeEnum.Student)
			{
				entity.Student.BirthDay = Convert.ToDateTime(userEditDto.BirthDay);
				entity.Student.Adress = userEditDto.Adress;
				entity.Student.PhoneNumber = userEditDto.PhoneNumber;
				
				if (userEditDto.ImagePath != null)
				{
					entity.Student.ImagePath = userEditDto.ImagePath;
				}
			}


			else if (entity.UserType == Data.Enums.UserTypeEnum.Parent)
			{
				entity.Parent.BirthDay = Convert.ToDateTime(userEditDto.BirthDay);
				entity.Parent.Adress = userEditDto.Adress;
				entity.Parent.PhoneNumber = userEditDto.PhoneNumber;
				entity.Parent.Email = userEditDto.Email;

				if (userEditDto.ImagePath != null)
				{
					entity.Parent.ImagePath = userEditDto.ImagePath;
				}
			}

			_userRepository.Update(entity);
		}

		public UserEditDto GetUser(int id)
		{
			var entityList = _userRepository.GetAll();
			var entity = entityList
				.Include(x=>x.Person)
				.Include(x=>x.Student)
				.Include(x=>x.Parent)
				.FirstOrDefault(x => x.Id == id);


			var userDto = new UserEditDto();

			if ((entity.UserType==Data.Enums.UserTypeEnum.Admin) || (entity.UserType == Data.Enums.UserTypeEnum.Person) || entity.UserType==Data.Enums.UserTypeEnum.Teacher)
			{
				userDto.Id = entity.Id;
				//userDto.PersonId = entity.Person.Id;
				userDto.Email = entity.Person.Email;
				userDto.FirstName = entity.Person.FirstName;
				userDto.IdentityNumber = entity.Person.IdentityPerson;
				userDto.ImagePath = entity.Person.ImagePath;
				userDto.LastName = entity.Person.LastName;
				userDto.Adress = entity.Person.Adress;
				userDto.BirthDay = entity.Person.BirthDay;
				userDto.PhoneNumber = entity.Person.PhoneNumber;
			}

			else if(entity.UserType==Data.Enums.UserTypeEnum.Student)
			{
				userDto.Id = entity.Id;
				
				userDto.FirstName = entity.Student.FirstName;
				userDto.IdentityNumber = entity.Student.IdentityPerson;
				userDto.ImagePath = entity.Student.ImagePath;
				userDto.LastName = entity.Student.LastName;
				userDto.Adress = entity.Student.Adress;
				userDto.BirthDay = entity.Student.BirthDay;
				userDto.PhoneNumber = entity.Student.PhoneNumber;
			}

			else if (entity.UserType == Data.Enums.UserTypeEnum.Parent)
			{
				userDto.Id = entity.Id;
				
				userDto.FirstName = entity.Parent.FirstName;
				userDto.IdentityNumber = entity.Parent.IdentityPerson;
				userDto.ImagePath = entity.Parent.ImagePath;
				userDto.LastName = entity.Parent.LastName;
				userDto.Adress = entity.Parent.Adress;
				userDto.BirthDay = entity.Parent.BirthDay;
				userDto.PhoneNumber = entity.Parent.PhoneNumber;
				userDto.Email= entity.Parent.Email;
			}




			return userDto;
		
			
		}

		public UserDto LoginPerson(LoginDto loginDto)
		{
			var userList = _userRepository.GetAll();
			var userEntity = userList
				.Include(x=>x.Person)
				.Include(x=>x.Student)
				.Include(x=>x.Parent)
				.FirstOrDefault(x => x.UserName == loginDto.UserName);
			if (userEntity == null)
			{
				return null;
			}
			var rawPassword = _dataProtector.Unprotect(userEntity.Password);
			if (rawPassword != loginDto.Password)
			{
				return null;
			}
			else
			{
				var userDto = new UserDto();
				if (userEntity.UserType==Data.Enums.UserTypeEnum.Admin)
				{
					


					userDto.UserName = userEntity.UserName;
					userDto.FirstName = userEntity.Person.FirstName;
					userDto.LastName = userEntity.Person.LastName;
					userDto.ImagePath = userEntity.Person.ImagePath;
					userDto.IdentityPerson = userEntity.Person.IdentityPerson;
					userDto.Email = userEntity.Person.Email;
					userDto.Id = userEntity.Id;
					userDto.UserType = Data.Enums.UserTypeEnum.Admin;
					
				}
				else if (userEntity.UserType == Data.Enums.UserTypeEnum.Person)
				{
					userDto.UserName = userEntity.UserName;
					userDto.FirstName = userEntity.Person.FirstName;
					userDto.LastName = userEntity.Person.LastName;
					userDto.ImagePath = userEntity.Person.ImagePath;
					userDto.IdentityPerson = userEntity.Person.IdentityPerson;
					userDto.Email = userEntity.Person.Email;
					userDto.Id = userEntity.Id;
					userDto.UserType = Data.Enums.UserTypeEnum.Person;
				}
				else if (userEntity.UserType == Data.Enums.UserTypeEnum.Student)
				{
					userDto.UserName = userEntity.UserName;
					userDto.FirstName = userEntity.Student.FirstName;
					userDto.LastName = userEntity.Student.LastName;
					userDto.ImagePath = userEntity.Student.ImagePath;
					userDto.IdentityPerson = userEntity.Student.IdentityPerson;
					
					userDto.Id = userEntity.Id;
					userDto.UserType = Data.Enums.UserTypeEnum.Student;
				}
				else if (userEntity.UserType == Data.Enums.UserTypeEnum.Parent)
				{
					userDto.UserName = userEntity.UserName;
					userDto.FirstName = userEntity.Parent.FirstName;
					userDto.LastName = userEntity.Parent.LastName;
					userDto.ImagePath = userEntity.Parent.ImagePath;
					userDto.IdentityPerson = userEntity.Parent.IdentityPerson;

					userDto.Id = userEntity.Id;
					userDto.UserType = Data.Enums.UserTypeEnum.Parent;
				}
				else if (userEntity.UserType == Data.Enums.UserTypeEnum.Teacher)
				{
					userDto.UserName = userEntity.UserName;
					userDto.FirstName = userEntity.Person.FirstName;
					userDto.LastName = userEntity.Person.LastName;
					userDto.ImagePath = userEntity.Person.ImagePath;
					userDto.IdentityPerson = userEntity.Person.IdentityPerson;
					userDto.Email = userEntity.Person.Email;
					userDto.Id = userEntity.Id;
					userDto.UserType = Data.Enums.UserTypeEnum.Teacher;
				}


				return userDto;





			}
		}

		//public void UserGetById(int id)
		//{

		//	var entity = _userRepository.GetAll(x => x.Id == id);
		//	var classroom = entity
		//		.Include(x => x.Student)
		//		.ThenInclude(x => x.ClassRoomStudents)
		//		.Select(x => new ClassRoomStudentListDto()
		//		{
		//			FirstName = x.Student.FirstName,
		//			LastName = x.Student.LastName,
		//			StudentId = x.Student.Id,
		//			ClassRoomId = x.Student.ClassRoomStudents.Select(x => x.ClassRoom.Id).First(),
		//			ClassRoom = x.Student.ClassRoomStudents.Select(x => x.ClassRoom.Name).First(),
		//		}).ToList();





		//}
	}
}
