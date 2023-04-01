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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Managers
{
	public class StudentManager :IStudentService
	{
		private readonly IRepository<StudentEntity> _studentRepository;
		private readonly IRepository<UserEntity> _userReporsitory;
		private readonly IDataProtector _dataProtector;
		public StudentManager(IRepository<StudentEntity> studentRepository,IDataProtectionProvider dataProtectionProvider, IRepository<UserEntity> userReporsitory)
		{
			_studentRepository = studentRepository;
			_dataProtector = dataProtectionProvider.CreateProtector("security");
			_userReporsitory= userReporsitory;
		}

		public ServiceMessage AddStudent(StudentParentDto studentParentDto)
		{
			var hasStudent = _studentRepository.GetAll(x => x.IdentityPerson == studentParentDto.StudentIdentityPerson);
			if (hasStudent.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu öğrenci zaten mevcut"
				};
			}

			var studentEntity = new StudentEntity()
			{
				Adress = studentParentDto.StudentAdress.ToUpper(),
				AreaInformation = studentParentDto.AreaInformation.ToUpper(),
				BirthDay = studentParentDto.StundetBirthDay,
				BirthPlace = studentParentDto.StudentBirthPlace.Trim().ToUpper(),
				ClassRoom = studentParentDto.ClassRoom.ToUpper(),
				FirstName = studentParentDto.StundetFirstName.Trim().ToUpper(),
				LastName = studentParentDto.ParentLastName.Trim().ToUpper(),
				Gender = studentParentDto.ParentGender,
				IdentityPerson = studentParentDto.StudentIdentityPerson.Trim(),
				ImagePath = studentParentDto.StudentImagePath,
				UserName = studentParentDto.StudentIdentityPerson.Trim(),
				Password =_dataProtector.Protect( studentParentDto.StudentIdentityPerson.Trim()),
				PhoneNumber = studentParentDto.StudentPhoneNumber.Trim(),
				StudentNumber = studentParentDto.StudentNumber.Trim(),
				UserType = Data.Enums.UserTypeEnum.Student,
				Grade = studentParentDto.StudentGrade,
				ParentId = studentParentDto.ParentId,
				User = new UserEntity()
				{
					UserName = studentParentDto.StudentIdentityPerson,
					Password = _dataProtector.Protect(studentParentDto.StudentIdentityPerson.Trim()),
					UserType = UserTypeEnum.Student,
				},

			};
			
			_studentRepository.Add(studentEntity);

			return new ServiceMessage()
			{
				IsSucceed = true,
			};
		}

		public void DeleteStudent(int id)
		{
			_studentRepository.Delete(id);
		}

		public ServiceMessage EditStudent(StudentEditDto studentEditDto)
		{

			var entityStudent = _studentRepository.GetById(studentEditDto.Id);

			var entities = _studentRepository.GetAll(x => x.IdentityPerson == studentEditDto.StudentIdentityPerson && x.IdentityPerson != entityStudent.IdentityPerson);
			if (entities.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu kişi zaten mevcut"
				};
			}
			var entity = _studentRepository.GetAll()
				.Include(x => x.User)
				.FirstOrDefault(x => x.Id == studentEditDto.Id);

			entity.Adress = studentEditDto.StudentAdress.ToUpper();
			entity.AreaInformation = studentEditDto.AreaInformation.ToUpper();
			entity.BirthDay = studentEditDto.StundetBirthDay;
			entity.BirthPlace = studentEditDto.StudentBirthPlace.Trim().ToUpper();
			entity.ClassRoom = studentEditDto.ClassRoom.ToUpper();
			entity.FirstName = studentEditDto.StundetFirstName.Trim().ToUpper();
			entity.LastName = studentEditDto.ParentLastName.Trim().ToUpper();
			entity.Gender = studentEditDto.ParentGender;
			entity.IdentityPerson = studentEditDto.StudentIdentityPerson.Trim();

			entity.UserName = studentEditDto.StudentIdentityPerson.Trim();
			entity.PhoneNumber = studentEditDto.StudentPhoneNumber.Trim();
			entity.StudentNumber = studentEditDto.StudentNumber.Trim();
			entity.UserType = Data.Enums.UserTypeEnum.Student;
			entity.Grade = studentEditDto.StudentGrade;
			entity.ParentId = studentEditDto.ParentId;
			entity.Password = _dataProtector.Protect(studentEditDto.StudentIdentityPerson.Trim());
			if (studentEditDto.StudentImagePath != null)
			{
				entity.ImagePath = studentEditDto.StudentImagePath;
			}

			entity.User.UserName = studentEditDto.StudentIdentityPerson.Trim();
			if (studentEditDto.StudentPassword != null)
			{
				entity.User.Password = _dataProtector.Protect(studentEditDto.StudentPassword);
				entity.Password = _dataProtector.Protect(studentEditDto.StudentPassword);
			}



			_studentRepository.Update(entity);

			return new ServiceMessage()
			{
				IsSucceed = true,
			};

			//var entityList = _studentRepository.GetAll();
			//var entity = entityList
			//	.Include(x => x.User)
			//	.FirstOrDefault(x => x.Id == studentEditDto.Id);

			//entity.Adress = studentEditDto.StudentAdress.ToUpper();
			//entity.AreaInformation = studentEditDto.AreaInformation.ToUpper();
			//entity.BirthDay = studentEditDto.StundetBirthDay;
			//entity.BirthPlace = studentEditDto.StudentBirthPlace.Trim().ToUpper();
			//entity.ClassRoom = studentEditDto.ClassRoom.ToUpper();
			//entity.FirstName = studentEditDto.StundetFirstName.Trim().ToUpper();
			//entity.LastName = studentEditDto.ParentLastName.Trim().ToUpper();
			//entity.Gender = studentEditDto.ParentGender;
			//entity.IdentityPerson = studentEditDto.StudentIdentityPerson.Trim();

			//entity.UserName = studentEditDto.StudentIdentityPerson.Trim();
			//entity.PhoneNumber = studentEditDto.StudentPhoneNumber.Trim();
			//entity.StudentNumber = studentEditDto.StudentNumber.Trim();
			//entity.UserType = Data.Enums.UserTypeEnum.Student;
			//entity.Grade = studentEditDto.StudentGrade;
			//entity.ParentId = studentEditDto.ParentId;
			//entity.Password = _dataProtector.Protect(studentEditDto.StudentIdentityPerson.Trim());
			//if (studentEditDto.StudentImagePath!=null)
			//{
			//	entity.ImagePath = studentEditDto.StudentImagePath;
			//}

			//entity.User.UserName =studentEditDto.StudentIdentityPerson.Trim();
			//if (studentEditDto.StudentPassword!=null)
			//{
			//	entity.User.Password = _dataProtector.Protect(studentEditDto.StudentPassword);
			//	entity.Password = _dataProtector.Protect(studentEditDto.StudentPassword);
			//}



			//_studentRepository.Update(entity);



		}

		public List<StudentListDto> GetAll()
        {
            var studentEntity = _studentRepository.GetAll()
				.Include(x=>x.ClassRoomStudents)
				.ThenInclude(x=>x.ClassRoom)
				.OrderBy(x=>x.ClassRoom).ThenBy(x=>x.FirstName);

			var studentDto = studentEntity.Select(x=> new StudentListDto()
			{
				Id= x.Id,
				StdentFirstName= x.FirstName,
				StudentLastName= x.LastName,
				Grade= x.ClassRoom,
				ImagePath=x.ImagePath,
				ParentFirstName=x.Parent.FirstName,
				ParentLastName= x.Parent.LastName,
				ParentIdentity=x.Parent.IdentityPerson,
				StudentIdentity=x.IdentityPerson,
				StudentPassword=_dataProtector.Unprotect(x.Password),
				StudentUserName=x.UserName,
				ClassRoom=x.ClassRoomStudents.FirstOrDefault(a=>a.StudentId==x.Id).ClassRoom.Name
			}).ToList();
			return studentDto;
        }

		public StudentDetailDto GetById(int id)
		{
			var entity = _studentRepository.GetAll(x=>x.Id==id);

			var dto = entity.Select(x => new StudentDetailDto()
			{
				
				AreaInformation = x.AreaInformation,
				ClassRoom = x.ClassRoom,
				ParentAdress = x.Parent.Adress,
				ParentFirstName = x.Parent.FirstName,
				ParentLastName = x.Parent.LastName,
				ParentBirthDay = x.Parent.BirthDay,
				ParentBirthPlace = x.Parent.BirthPlace,
				ParentEmail = x.Parent.Email,
				ParentIdentity = x.Parent.IdentityPerson,
				ParentJob = x.Parent.Job,
				ParentPhoneNumber = x.Parent.PhoneNumber,
				StudentAdress = x.Adress,
				StudentBirthPlace = x.BirthPlace,
				StudentGrade = x.Grade,
				StudentIdentityPerson = x.IdentityPerson,
				StudentLastName = x.LastName,
				StudentNumber = x.StudentNumber,
				StudentPhoneNumber = x.PhoneNumber,
				StundetBirthDay = x.BirthDay,
				StundetFirstName = x.LastName,
				StudentUserName = x.UserName,
				StudentPassword = _dataProtector.Unprotect(x.Password),
				ParentUserName = x.Parent.UserName,
				ParentPassword = _dataProtector.Unprotect(x.Parent.Password),
				

			}).ToList();
			return dto[0];
		}

		public StudentEditDto GetByStudent(int id)
		{
			var entity = _studentRepository.GetAll(x=>x.Id==id);
			var dto = entity.Select(x => new StudentEditDto()
			{
				Id=x.Id,
				AreaInformation = x.AreaInformation,
				ClassRoom = x.ClassRoom,
				ParentAdress = x.Parent.Adress,
				ParentFirstName = x.Parent.FirstName,
				ParentLastName = x.Parent.LastName,
				ParentBirthDay = x.Parent.BirthDay,
				ParentBirthPlace = x.Parent.BirthPlace,
				ParentEmail = x.Parent.Email,
				ParentIdentity = x.Parent.IdentityPerson,
				ParentJob = x.Parent.Job,
				ParentPhoneNumber = x.Parent.PhoneNumber,
				StudentAdress = x.Adress,
				StudentBirthPlace = x.BirthPlace,
				StudentGrade = x.Grade,
				StudentIdentityPerson = x.IdentityPerson,
				StudentLastName = x.LastName,
				StudentNumber = x.StudentNumber,
				StudentPhoneNumber = x.PhoneNumber,
				StundetBirthDay = x.BirthDay,
				StundetFirstName = x.FirstName,
				StudentUserName = x.UserName,
				StudentPassword = _dataProtector.Unprotect(x.Password),
				ParentUserName = x.Parent.UserName,
				ParentPassword = _dataProtector.Unprotect(x.Parent.Password),
				ParentGender= x.Gender,
				StudentGender= x.Gender,
				StudentImagePath= x.ImagePath,
				ParentId=x.ParentId

			}).ToList();
			return dto[0];
		}
	}
}
