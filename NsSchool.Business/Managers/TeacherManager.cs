using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.Business.Types;
using NsSchool.Data.Entites;
using NsSchool.Data.Enums;
using NsSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Managers
{
	public class TeacherManager : ITeacherService
	{
		private readonly IRepository<PersonEntity> _teacherRepository;
		private readonly IRepository<UserEntity> _userRepository;
		private readonly IDataProtector _dataProtector;
		public TeacherManager(IRepository<PersonEntity> teacherRepository, IDataProtectionProvider dataProtectionProvider, IRepository<UserEntity> userRepository)
		{
			_teacherRepository = teacherRepository;
			_dataProtector = dataProtectionProvider.CreateProtector("security");
			_userRepository = userRepository;
		}

		public ServiceMessage AddTeacher(TeacherAddDto teacherAddDto)
		{
			var hasTeacher = _teacherRepository.GetAll(x => x.IdentityPerson == teacherAddDto.IdentityPerson);
			if (hasTeacher.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu kullanıcı zaten mevcut"

				};
			}

			var entity = new PersonEntity()
			{
				Adress = teacherAddDto.Adress.ToUpper(),
				BirthDay = teacherAddDto.BirthDay,
				BirthPlace = teacherAddDto.BirthPlace.Trim().ToUpper(),
				Branch = teacherAddDto.Branch,
				Email = teacherAddDto.Email.Trim().ToLower(),
				FirstName = teacherAddDto.FirstName.ToUpper(),
				Gender = teacherAddDto.Gender,
				IdentityPerson = teacherAddDto.IdentityPerson.Trim(),
				ImagePath = teacherAddDto.ImagePath,
				LastName = teacherAddDto.LastName.ToUpper(),
				UserName = teacherAddDto.IdentityPerson,
				Password = _dataProtector.Protect(teacherAddDto.IdentityPerson),
				PhoneNumber = teacherAddDto.PhoneNumber,
				UserType = Data.Enums.UserTypeEnum.Teacher,
				User = new UserEntity()
				{
					UserName = teacherAddDto.IdentityPerson,
					Password = _dataProtector.Protect(teacherAddDto.IdentityPerson.Trim()),
					UserType = UserTypeEnum.Teacher,
				},

			};
			_teacherRepository.Add(entity);

			//var userEntity = new UserEntity()
			//{
			//	UserName = teacherAddDto.IdentityPerson.Trim(),
			//	Password = _dataProtector.Protect(teacherAddDto.IdentityPerson),
			//	UserType = Data.Enums.UserTypeEnum.Teacher
			//};
			//_userRepository.Add(userEntity);

			return new ServiceMessage()
			{
				IsSucceed = true
			};

		}

		public void DeleteTeacher(int id)
		{
			_teacherRepository.Delete(id);
			
		}

		public ServiceMessage EditTeacher(TeacherEditDto teacherEditDto)
		{
			var entityTeacher = _teacherRepository.GetById(teacherEditDto.Id);

			var entities = _teacherRepository.GetAll(x => x.IdentityPerson == teacherEditDto.IdentityPerson && x.IdentityPerson != entityTeacher.IdentityPerson);
			if (entities.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu kişi zaten mevcut"
				};
			}
			var entity = _teacherRepository.GetAll()
				.Include(x => x.User)
				.FirstOrDefault(x => x.Id == teacherEditDto.Id);


			entity.BirthDay = teacherEditDto.BirthDay;
			entity.Branch = teacherEditDto.Branch;
			entity.Email = teacherEditDto.Email;
			entity.FirstName = teacherEditDto.FirstName;
			entity.LastName = teacherEditDto.LastName;
			entity.Adress = teacherEditDto.Adress;
			entity.Gender = teacherEditDto.Gender;
			entity.IdentityPerson = teacherEditDto.IdentityPerson;
			entity.PhoneNumber = teacherEditDto.PhoneNumber;
			entity.BirthPlace = teacherEditDto.BirthPlace;
			entity.ImagePath = teacherEditDto.ImagePath;
			entity.UserName = teacherEditDto.UserName;
			entity.User.UserName = teacherEditDto.UserName;
			entity.User.Password = _dataProtector.Protect(teacherEditDto.IdentityPerson);
			entity.Password = _dataProtector.Protect(teacherEditDto.IdentityPerson);

			if (teacherEditDto.ImagePath != null)
			{
				entity.ImagePath = teacherEditDto.ImagePath;
			}

			if (teacherEditDto.Password != null)
			{
				entity.Password = _dataProtector.Protect(teacherEditDto.Password);
				entity.User.Password =_dataProtector.Protect(teacherEditDto.Password);
			}


			_teacherRepository.Update(entity);
			return new ServiceMessage()
			{
				IsSucceed = true,
			};


			//var entity = _teacherRepository.GetById(teacherEditDto.Id);



			//var entityList = _teacherRepository.GetAll();
			//var getEntity = entityList
			//	.Include(x => x.User)
			//	.FirstOrDefault(x => x.Id == teacherEditDto.Id);

			//getEntity.BirthDay = teacherEditDto.BirthDay;
			//getEntity.Branch = teacherEditDto.Branch;
			//getEntity.Email = teacherEditDto.Email;
			//getEntity.FirstName = teacherEditDto.FirstName;
			//getEntity.LastName = teacherEditDto.LastName;
			//getEntity.Adress = teacherEditDto.Adress;
			//getEntity.Gender = teacherEditDto.Gender;
			//getEntity.IdentityPerson = teacherEditDto.IdentityPerson;
			//getEntity.PhoneNumber = teacherEditDto.PhoneNumber;
			//getEntity.BirthPlace = teacherEditDto.BirthPlace;
			//getEntity.ImagePath = teacherEditDto.ImagePath;
			//getEntity.UserName = teacherEditDto.UserName;
			//getEntity.User.UserName = teacherEditDto.UserName;
			//getEntity.User.Password = _dataProtector.Protect(teacherEditDto.IdentityPerson);
			//getEntity.Password = _dataProtector.Protect(teacherEditDto.IdentityPerson);

			//if (teacherEditDto.ImagePath!=null)
			//{
			//	getEntity.ImagePath = teacherEditDto.ImagePath;
			//}

			//if (teacherEditDto.Password != null)
			//{
			//	getEntity.Password = _dataProtector.Protect(teacherEditDto.Password);
			//	getEntity.User.Password = getEntity.Password = _dataProtector.Protect(teacherEditDto.Password);
			//}


			//_teacherRepository.Update(getEntity);




		}

		public TeacherEditDto GetById(int id)
		{
			var entity = _teacherRepository.GetById(id);
			var teacherEditDto = new TeacherEditDto()
			{
				Adress = entity.Adress,
				BirthDay = entity.BirthDay,
				BirthPlace = entity.BirthPlace,
				Branch = entity.Branch,
				Email = entity.Email,
				FirstName = entity.FirstName,
				LastName = entity.LastName,
				Gender = entity.Gender,
				Id = entity.Id,
				IdentityPerson = entity.IdentityPerson,
				ImagePath = entity.ImagePath,
				PhoneNumber = entity.PhoneNumber,
				UserName = entity.UserName,
				UserId = entity.UserId,
			};
			return teacherEditDto;
		}

		public List<TeacherListDto> GetTeacherList()
		{
			var entity = _teacherRepository.GetAll().OrderBy(x => x.FirstName).ThenBy(x => x.Branch);

			var teacherDtos = entity.Where(x => x.UserType == Data.Enums.UserTypeEnum.Teacher && x.IsDeleted == false).Select(x => new TeacherListDto()
			{
				Branch = x.Branch.ToUpper(),
				FirstName = x.FirstName,
				Email = x.Email,

				IdentityPerson = x.IdentityPerson,
				ImagePath = x.ImagePath,
				LastName = x.LastName,
				Password = _dataProtector.Unprotect(x.Password),
				PhoneNumber = x.PhoneNumber,
				UserName = x.UserName,
				UserType = x.UserType,
				Id = x.Id

			}).ToList();
			return teacherDtos;
		}
	}
}
