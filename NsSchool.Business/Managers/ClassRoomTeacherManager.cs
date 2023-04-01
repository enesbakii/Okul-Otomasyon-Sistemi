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
	public class ClassRoomTeacherManager:IClassRoomTeacherService
	{
		private readonly IRepository<ClassRoomTeacherEntity> _classTeacherRepository;
		private readonly IRepository<ClassRoomStudentEntity> _classRoomStudentRepository;
		public ClassRoomTeacherManager(IRepository<ClassRoomTeacherEntity> classTeacherRepository, IRepository<ClassRoomStudentEntity> classRoomStudentRepository)
		{
			_classTeacherRepository = classTeacherRepository;
			_classRoomStudentRepository= classRoomStudentRepository;
		}

		public ServiceMessage AddClassRoomTeacher(List<ClassRoomTeacherDto> classRoomTeacherDtos)
		{
		
			var classRoomTeacherEntityList = new List<ClassRoomTeacherEntity>();
			foreach(var item in classRoomTeacherDtos)
			{
				var hasEntity = _classTeacherRepository.GetAll(x => x.ClassRoomId == item.ClassRoomId && x.PersonId==item.TeacherId);
				if (hasEntity.Any())
				{
					return new ServiceMessage()
					{
						IsSucceed = false,
						Message = "Bu kişi bu sınıfta zaten mevcut"
					};
				}

				var classroomTeacherEntity = new ClassRoomTeacherEntity()
				{
					ClassRoomId = item.ClassRoomId,
					PersonId = item.TeacherId
				};
				classRoomTeacherEntityList.Add(classroomTeacherEntity);
			}


			_classTeacherRepository.BulkAdd(classRoomTeacherEntityList);
			return new ServiceMessage()
			{
				IsSucceed = true
			};
		}

		public List<ClassRoomTeacherListDto> ClassRoomTeacherList(int id)
		{
			var entity = _classTeacherRepository.GetAll(x => x.ClassRoomId == id);
			var dto = entity
				.Include(x=>x.Person)
				.Where(x=>x.IsDeleted==false)
				.Select(x=> new ClassRoomTeacherListDto()
			{
				ClassRoomId= x.ClassRoomId,
				Branch=x.Person.Branch,
				FirstName= x.Person.FirstName,
				LastName= x.Person.LastName,
				TeacherId=x.PersonId
			}).ToList();
			return dto;
		}

		public void DeleteTeacher(ClassRoomTeacherDto classRoomTeacherDto)
		{
			var classEntity = _classTeacherRepository.Get(x => x.ClassRoomId == classRoomTeacherDto.ClassRoomId&& x.PersonId==classRoomTeacherDto.TeacherId);
			

			_classTeacherRepository.HardDelete(classEntity);
		}

		public List<StudentsClassRoomsDto> ListStudent(int id)
		{
			var entity = _classTeacherRepository.GetAll(x => x.Person.UserId == id).ToList();
			var studentList =new List<StudentsClassRoomsDto>();
			foreach (var item in entity)
			{
				
				var dto = _classRoomStudentRepository.GetAll(x => x.ClassRoomId == item.ClassRoomId)
					.Include(x=>x.Student).Select(x=> new StudentsClassRoomsDto()

					{
						
						Students=new ClassRoomStudentListDto()
						{
							FirstName=x.Student.FirstName,
							LastName=x.Student.LastName,
							ClassRoom=x.ClassRoom.Name,
							ClassRoomId=item.ClassRoomId,

						},
						ClassRoom=x.ClassRoom.Name,
						
					}).ToList();
			
				
				
				if (dto.Any())
				{
					studentList.AddRange(dto);
				}
			
				
			}



			return studentList;




		}

		public List<ClassRoomTeacherListDto> ListTeacher(int id)
        {
            var entity = _classRoomStudentRepository.Get(x => x.Student.UserId == id);
            var teacherClass = _classTeacherRepository.GetAll(x => x.ClassRoomId == entity.ClassRoomId);
            var dto = teacherClass.Select(x => new ClassRoomTeacherListDto()
            {
                FirstName=x.Person.FirstName,
				LastName=x.Person.LastName,
				Branch=x.Person.Branch,
				TeacherId=x.Person.Id,
            }).ToList();
            return dto;
        }
    }
}
