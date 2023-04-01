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
	public class ClassRoomManager:IClassRoomService
	{
		private readonly IRepository<ClassRoomEntity> _classRoomRepository;
		
		
		public ClassRoomManager(IRepository<ClassRoomEntity> classRoomRepository)
		{
			_classRoomRepository = classRoomRepository;
			
		}

		public ServiceMessage AddClassRoom(ClassRoomDto classRoomDto)
		{
			var hasClassRoom = _classRoomRepository.GetAll(x=>x.Name==classRoomDto.Name);
			if (hasClassRoom.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu sınıf adı zaten mevcut"
				};


			}

			var entity = new ClassRoomEntity()
			{
				Name = classRoomDto.Name,
			};

			_classRoomRepository.Add(entity);

			return new ServiceMessage()
			{
				IsSucceed = true,
			};
			
		}

		

		public void DeleteClassRoom(int id)
		{
			_classRoomRepository.Delete(id);
		}

		public ClassRoomDto GetClassRoomById(int id)
		{
			var entity = _classRoomRepository.GetById(id);
			var classRoomDto = new ClassRoomDto()
			{
				Id = entity.Id,
				Name = entity.Name,
			};
			return classRoomDto;
		}

		public List<ClassRoomDto> GetClassRoomList()
		{
			var entitiy = _classRoomRepository.GetAll();

			var classRoomDtos = entitiy.Select(x=> new ClassRoomDto()
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();

			return classRoomDtos;
		}

		public ServiceMessage UpdateClassRoom(ClassRoomDto classRoomDto)
		{
			var hasClassRoom = _classRoomRepository.GetAll(x => x.Name == classRoomDto.Name);
			if (!hasClassRoom.Any())
			{
				var entity = _classRoomRepository.GetById(classRoomDto.Id);

				entity.Name= classRoomDto.Name;
				_classRoomRepository.Update(entity);
				return new ServiceMessage()
				{
					IsSucceed = true,
				};

			}
			return new ServiceMessage()
			{
				IsSucceed = false,
				Message = "Bu sınıf adı zaten mevcut"
			};
		}
	}
}
