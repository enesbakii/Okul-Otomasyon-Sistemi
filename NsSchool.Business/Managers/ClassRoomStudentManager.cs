using Microsoft.EntityFrameworkCore;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.Business.Types;
using NsSchool.Data.Entites;
using NsSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Managers
{
	public class ClassRoomStudentManager : IClassRoomStudentService
	{
		private readonly IRepository<ClassRoomStudentEntity> _classRoomStudentRepository;
		public ClassRoomStudentManager(IRepository<ClassRoomStudentEntity> classRoomStudentRepository)
		{
			_classRoomStudentRepository = classRoomStudentRepository;
		}

		public ServiceMessage AddStudent(List<ClassRoomStudentDto> classRoomStudentDtos)
		{
			var classRoomStundentsList = new List<ClassRoomStudentEntity>();
			foreach (var item in classRoomStudentDtos)
			{
				var hasEntity = _classRoomStudentRepository.GetAll(x => x.StudentId == item.StudentId);
				if (hasEntity.Any())
				{
					return new ServiceMessage()
					{
						IsSucceed = false,
						Message = "Bu öğrenci farklı bir sınıfta mevcut"
					};
				}

				var classRoomStudentEntity = new ClassRoomStudentEntity()
				{
					StudentId = item.StudentId,
					ClassRoomId = item.ClassRoomId,
				};
				classRoomStundentsList.Add(classRoomStudentEntity);
			}
			_classRoomStudentRepository.BulkAdd(classRoomStundentsList);
			return new ServiceMessage()
			{
				IsSucceed = true
			};
		}

		public List<ClassRoomStudentListDto> ClassRoomStudentList(int id)
		{
			var entity = _classRoomStudentRepository.GetAll(x=>x.ClassRoomId==id);
			var dto = entity
				.Include(x=>x.Student)
				.Select(x=> new ClassRoomStudentListDto()
			{
				FirstName=x.Student.FirstName,
				LastName=x.Student.LastName,
				ClassRoomId=x.ClassRoomId,
				ClassRoom=x.ClassRoom.Name,
				StudentId=x.StudentId,
			}).ToList();

			return dto;
		}

		public void DeleteStudent(ClassRoomStudentDto classRoomStudentDto)
		{
			var classEntity = _classRoomStudentRepository.Get(x => x.ClassRoomId == classRoomStudentDto.ClassRoomId && x.StudentId == classRoomStudentDto.StudentId);


			_classRoomStudentRepository.HardDelete(classEntity);
		}

        public List<ClassRoomStudentListDto> ListStudent(int id)
        {

			var entity = _classRoomStudentRepository.Get(x => x.Student.UserId == id);
			var studentclass = _classRoomStudentRepository.GetAll(x => x.ClassRoomId == entity.ClassRoomId);
			var dto = studentclass.Select(x => new ClassRoomStudentListDto()
			{
				ClassRoom = x.ClassRoom.Name,
				FirstName = x.Student.FirstName,
				ClassRoomId = x.ClassRoomId,
				LastName = x.Student.LastName
			}).ToList();
			return dto;
        }
    }
}
