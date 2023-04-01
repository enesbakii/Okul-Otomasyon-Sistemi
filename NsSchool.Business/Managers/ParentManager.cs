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
using System.Xml.Schema;

namespace NsSchool.Business.Managers
{
	public class ParentManager : IParentService
	{
		private readonly IRepository<ParentEntity> _parentRepository;
		private readonly IRepository<UserEntity> _userRepository;
		private readonly IRepository<StudentEntity> _stundetRepository;
		private readonly IDataProtector _dataProtector;
		public ParentManager(IRepository<ParentEntity> parentRepository, IDataProtectionProvider dataProtectionProvider, IRepository<UserEntity> userRepository, IRepository<StudentEntity> stundetRepository)
		{
			_parentRepository = parentRepository;
			_dataProtector = dataProtectionProvider.CreateProtector("security");
			_userRepository = userRepository;
			_stundetRepository = stundetRepository;
		}

		public ServiceMessage AddParent(StudentParentDto studentParentDto)
		{
			var hasParent = _parentRepository.GetAll(x => x.IdentityPerson == studentParentDto.ParentIdentity);
			if (!hasParent.Any())
			{
				var parentEntitiy = new ParentEntity()
				{

					Adress = studentParentDto.ParentAdress.ToUpper(),
					BirthDay = studentParentDto.ParentBirthDay,
					BirthPlace = studentParentDto.ParentBirthPlace.ToUpper(),
					FirstName = studentParentDto.ParentFirstName.ToUpper().Trim(),
					LastName = studentParentDto.ParentLastName.ToUpper().Trim(),
					IdentityPerson = studentParentDto.ParentIdentity.Trim(),
					Email = studentParentDto.ParentEmail.ToLower().Trim(),
					Gender = studentParentDto.ParentGender,
					Job = studentParentDto.ParentJob.ToUpper(),
					PhoneNumber = studentParentDto.ParentPhoneNumber.Trim(),
					UserName = studentParentDto.ParentIdentity.Trim(),
					Password = _dataProtector.Protect(studentParentDto.ParentIdentity),
					UserType = Data.Enums.UserTypeEnum.Parent,
					User = new UserEntity()
					{
						UserName = studentParentDto.ParentIdentity.Trim(),
						Password = _dataProtector.Protect(studentParentDto.ParentIdentity),
						UserType = UserTypeEnum.Parent,
					},

				};
				_parentRepository.Add(parentEntitiy);

				return new ServiceMessage()
				{
					IsSucceed = true,
				};
			}
			else
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
				};
			}

		}

		public ServiceMessage EditParent(StudentEditDto parentEditDto)
		{
			var entityParent = _parentRepository.GetById(parentEditDto.ParentId);

			var entities = _parentRepository.GetAll(x => x.IdentityPerson == parentEditDto.ParentIdentity && x.IdentityPerson != entityParent.IdentityPerson);
			if (entities.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu kişi zaten mevcut"
				};
			}

			var entityList = _parentRepository.GetAll();
			var entity = entityList
				.Include(x => x.User)
				.FirstOrDefault(x => x.Id == parentEditDto.ParentId);
			entity.IdentityPerson = parentEditDto.ParentIdentity;
			entity.FirstName = parentEditDto.ParentFirstName;
			entity.LastName = parentEditDto.ParentLastName;
			entity.BirthDay = parentEditDto.ParentBirthDay;
			entity.Gender = parentEditDto.ParentGender;
			entity.Job = parentEditDto.ParentJob;
			entity.BirthPlace = parentEditDto.ParentBirthPlace;
			entity.PhoneNumber = parentEditDto.ParentPhoneNumber;
			entity.UserName = parentEditDto.ParentIdentity;
			entity.User.UserName= parentEditDto.ParentIdentity;
			entity.Password = _dataProtector.Protect(parentEditDto.ParentIdentity);
			entity.User.Password = _dataProtector.Protect(parentEditDto.ParentPassword);
			entity.Adress = parentEditDto.ParentAdress;
			entity.Email = parentEditDto.ParentEmail;

			if (parentEditDto.ParentPassword != null)
			{
				entity.Password = _dataProtector.Protect(parentEditDto.ParentPassword);
				entity.User.Password= _dataProtector.Protect(parentEditDto.ParentPassword);

			}

			_parentRepository.Update(entity);
			return new ServiceMessage()
			{
				IsSucceed = true,
			};

			//         var userEntity = _userRepository.GetById(studentEditDto.ParentIdentity);
			//         userEntity.UserName = studentEditDto.ParentUserName;
			//userEntity.Password = _dataProtector.Protect(studentEditDto.ParentPassword);
			//         _userRepository.Update(userEntity);

		}

		public StudentParentDto GetByParent()
		{
			var entities = _parentRepository.GetAll();
			if (!entities.Any())
			{
				return null;
			}
			else
			{
				var entitiy = entities.Last();
				var dto = new StudentParentDto()
				{
					ParentId = entitiy.Id
				};
				return dto;
			}


		}

		public StudentParentDto GetByParentIdentity(string parentIdentity)
		{
			var entity = _parentRepository.Get(x => x.IdentityPerson == parentIdentity);
			var dto = new StudentParentDto()
			{
				ParentId = entity.Id
			};
			return dto;
		}

		public List<ClassRoomStudentListDto> GetStudents(int id)
		{
			var entity = _parentRepository.Get(x => x.UserId == id);

			var entities = _stundetRepository.GetAll(x => x.ParentId == entity.Id);
				
			var dto = entities.Select(x => new ClassRoomStudentListDto()
				{
					FirstName = x.FirstName,

					LastName =x.LastName

			}).ToList();
			return dto;
		}
	}
}
