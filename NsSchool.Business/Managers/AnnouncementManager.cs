using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.Business.Types;
using NsSchool.Data.Entites;
using NsSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Managers
{
	public class AnnouncementManager : IAnnouncementService
	{
		private readonly IRepository<AnnouncementEntity> _announcementRepositoty;
		public AnnouncementManager(IRepository<AnnouncementEntity> announcementRepositoty)
		{
			_announcementRepositoty = announcementRepositoty;
		}

		public ServiceMessage AddAnnouncement(AnnouncementDto announcementDto)
		{
			var entitiy = new AnnouncementEntity()
			{
				Title = announcementDto.Title,
				Discription = announcementDto.Discription,
				Path = announcementDto.Path,
			};
			_announcementRepositoty.Add(entitiy);
			return new ServiceMessage()
			{
				IsSucceed = true,
			};

		}

		public void DeleteAnnouncement(int id)
		{
			_announcementRepositoty.Delete(id);
		}

		public void EditAnnouncement(AnnouncementEditDto announcementEditDto)
		{
			var entity = _announcementRepositoty.GetById(announcementEditDto.Id);

			entity.Title = announcementEditDto.Title;
			entity.Discription = announcementEditDto.Discrpiton;

			entity.Path = announcementEditDto.Path;

			_announcementRepositoty.Update(entity);
		}

		public AnnouncementEditDto GetAnnouncement(int id)
		{
			var entity = _announcementRepositoty.GetById(id);

			var announcementEditDto = new AnnouncementEditDto()
			{
				Id = entity.Id,
				Discrpiton = entity.Discription,
				Title = entity.Title,
			};
			return announcementEditDto;
		}

		public List<AnnouncementDto> GetAnnouncementList()
		{
			var entity = _announcementRepositoty.GetAll().OrderByDescending(x => x.ModifiedDate == null ? x.CreatedDate : x.ModifiedDate);

			var announcementDto = entity.Select(x => new AnnouncementDto()
			{

				Id = x.Id,
				Discription = x.Discription,
				Path = x.Path,
				Title = x.Title,
				ModifiedDate = x.ModifiedDate,
				CreatedDate = x.CreatedDate,
				
			}).ToList();



			return announcementDto;
		}

		public AnnouncementDto GetByIdAnnouncement(int id)
		{
			var entity = _announcementRepositoty.GetById(id);
			var announcementDto = new AnnouncementDto()
			{
				Title = entity.Title,
				Path = entity.Path,
				CreatedDate = entity.CreatedDate,
				Discription = entity.Discription,
				Id = entity.Id,
				
			};
			if (entity.ModifiedDate!=null)
			{
				announcementDto.ModifiedDate = entity.ModifiedDate;
			}
			return announcementDto;
		}
	}
}
