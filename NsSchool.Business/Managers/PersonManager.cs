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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Managers
{
	public class PersonManager : IPersonService
	{
		private readonly IRepository<PersonEntity> _personRepository;
		private readonly IDataProtector _dataProtector;
		public PersonManager(IRepository<PersonEntity> personRepository, IDataProtectionProvider dataProtectionProvider)
		{
			_personRepository = personRepository;
			_dataProtector = dataProtectionProvider.CreateProtector("security");
		}

		public ServiceMessage AddPerson(PersonAddDto personAddDto)
		{
			var hasPerson = _personRepository.GetAll(x => x.IdentityPerson == personAddDto.IdentityPerson).ToList();
			if (hasPerson.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu kişi zaten mevcut"
				};
			}
			var encryptedPassword = _dataProtector.Protect(personAddDto.IdentityPerson);

			var personEntity = new PersonEntity()
			{
				Adress = personAddDto.Adress.ToUpper(),
				BirthDay = personAddDto.BirthDay,
				BirthPlace = personAddDto.BirthPlace.ToUpper(),
				Email = personAddDto.Email.ToLower(),
				FirstName = personAddDto.FirstName.ToUpper(),
				LastName = personAddDto.Lastname.ToUpper(),
				IdentityPerson = personAddDto.IdentityPerson,
				Gender = personAddDto.Gender,
				UserName = personAddDto.IdentityPerson,
				Password = encryptedPassword,
				ImagePath = personAddDto.ImagePath,
				UserType = UserTypeEnum.Person,
				PhoneNumber = personAddDto.PhoneNumber,
				User = new UserEntity()
				{
					UserName = personAddDto.IdentityPerson,
					Password = encryptedPassword,
					UserType = UserTypeEnum.Person,
				},

			};

			_personRepository.Add(personEntity);

			return new ServiceMessage()
			{
				IsSucceed = true
			};
		}

		
		

		public List<PersonListDto> GetPersonList()
		{
			var entity = _personRepository.GetAll().OrderBy(x => x.FirstName);


			var personListDto = entity.Where(x => x.UserType == UserTypeEnum.Person || x.UserType == UserTypeEnum.Admin).Where(x => x.IsDeleted == false).Select(x => new PersonListDto()

			{
				FirstName = x.FirstName,
				LastName = x.LastName,
				Branch = x.Branch,
				Email = x.Email,
				Id = x.Id,
				Gender = x.Gender,
				BirthDay = x.BirthDay,
				IdentityPerson = x.IdentityPerson,
				Password = _dataProtector.Unprotect(x.Password),
				UserName = x.UserName,
				UserType = x.UserType,
				ImagePath = x.ImagePath

			}).ToList();

			return personListDto;
		}

		

		public PersonEditDto GetPerson(int id)
		{
			var entitiy = _personRepository.GetById(id);

			var personEditDto = new PersonEditDto()
			{
				Id = entitiy.Id,
				Adress = entitiy.Adress,
				BirthDay = Convert.ToDateTime(entitiy.BirthDay),
				BirthPlace = entitiy.BirthPlace,
				Email = entitiy.Email,
				FistName = entitiy.FirstName,
				Gender = entitiy.Gender,
				IdentityPerson = entitiy.IdentityPerson,
				ImagePath = entitiy.ImagePath,
				LastName = entitiy.LastName,
				PhoneNumber = entitiy.PhoneNumber,
				UserName = entitiy.UserName,
			};
			return personEditDto;
		}

		public ServiceMessage EditPerson(PersonEditDto personEditDto)
		{
			var entityPerson = _personRepository.Get(x=>x.Id==personEditDto.Id);
			var entities = _personRepository.GetAll(x=>x.IdentityPerson==personEditDto.IdentityPerson && x.IdentityPerson!= entityPerson.IdentityPerson);
			if (entities.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu kişi zaten mevcut"
				};
			}
			var entity = _personRepository.GetAll()
				.Include(x => x.User)
				.FirstOrDefault(x=>x.Id==personEditDto.Id);


			entity.FirstName = personEditDto.FistName.ToUpper();
			entity.Adress = personEditDto.Adress.ToUpper();
			entity.IdentityPerson = personEditDto.IdentityPerson;
			entity.Gender = personEditDto.Gender;
			entity.Email = personEditDto.Email.ToLower();
			entity.BirthDay = personEditDto.BirthDay;
			entity.BirthPlace = personEditDto.BirthPlace.ToUpper();
			entity.LastName = personEditDto.LastName.ToUpper();
			entity.UserName = personEditDto.IdentityPerson;
			entity.PhoneNumber = personEditDto.PhoneNumber;

			entity.Password = _dataProtector.Protect(personEditDto.IdentityPerson);

			entity.User.UserName = personEditDto.IdentityPerson;
			entity.User.Password = _dataProtector.Protect(personEditDto.IdentityPerson);

			if (personEditDto.ImagePath != null)
			{
				entity.ImagePath = personEditDto.ImagePath;
			}
			if (personEditDto.Password != null)
			{
				var encryptedPassword = _dataProtector.Protect(personEditDto.Password);
				entity.Password = encryptedPassword;
				entity.User.Password = encryptedPassword;
			}

			_personRepository.Update(entity);

			return new ServiceMessage()
			{
				IsSucceed = true,
			};

			//var entityList = _personRepository.GetAll();
			//var entity = entityList
			//.Include(x => x.User)
			//.FirstOrDefault(x => x.Id == personEditDto.Id);



			//entity.FirstName = personEditDto.FistName.ToUpper();
			//entity.Adress = personEditDto.Adress.ToUpper();
			//entity.IdentityPerson = personEditDto.IdentityPerson;
			//entity.Gender = personEditDto.Gender;
			//entity.Email = personEditDto.Email.ToLower();
			//entity.BirthDay = personEditDto.BirthDay;
			//entity.BirthPlace = personEditDto.BirthPlace.ToUpper();
			//entity.LastName = personEditDto.LastName.ToUpper();
			//entity.UserName = personEditDto.IdentityPerson;
			//entity.PhoneNumber = personEditDto.PhoneNumber;

			//entity.Password = _dataProtector.Protect(personEditDto.IdentityPerson);

			//entity.User.UserName= personEditDto.IdentityPerson;
			//entity.User.Password= _dataProtector.Protect(personEditDto.IdentityPerson);

			//if (personEditDto.ImagePath != null)
			//{
			//	entity.ImagePath = personEditDto.ImagePath;
			//}
			//if (personEditDto.Password != null)
			//{
			//	var encryptedPassword = _dataProtector.Protect(personEditDto.Password);
			//	entity.Password = encryptedPassword;
			//	entity.User.Password= encryptedPassword;
			//}

			//_personRepository.Update(entity);




		}

		public void DeletePerson(int id)
		{
			_personRepository.Delete(id);
		}
	}
}
