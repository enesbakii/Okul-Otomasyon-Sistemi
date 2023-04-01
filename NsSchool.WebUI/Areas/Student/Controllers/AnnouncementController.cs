using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Student.Models;

namespace NsSchool.WebUI.Areas.Student.Controllers
{
	[Area("Student")]
	[Authorize(Roles ="Student")]

	public class AnnouncementController : Controller
	{
		private readonly IAnnouncementService _announcementService;
		public AnnouncementController(IAnnouncementService announcementService)
		{
			_announcementService = announcementService;
		}

		public IActionResult List()
		{
			var announcemnetDto = _announcementService.GetAnnouncementList();
			var viewModel = announcemnetDto.Select(x=> new AnnouncementViewModel()
			{
				Id=x.Id,
				CreatedDate=x.CreatedDate,
				Discripton=x.Discription,
				ModifiedDate=x.ModifiedDate,
				Path=x.Path,
				Title =x.Title.ToUpper(),
			}).ToList();
			return View(viewModel);
		}
		public IActionResult Details(int id) 
		{
			var dto = _announcementService.GetByIdAnnouncement(id);
			var viewModel = new AnnouncementViewModel()
			{
				Id = dto.Id,
				ModifiedDate = dto.ModifiedDate,
				CreatedDate = dto.CreatedDate,
				Discripton = dto.Discription,
				Path = dto.Path,
				Title = dto.Title.ToUpper(),
			};
			return View(viewModel);
		}
		public IActionResult Download(string filePath)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "announcement", "announcementPDF", filePath);

			var memory = new MemoryStream();
			using (var stream = new FileStream(path, FileMode.Open))
			{
				stream.CopyTo(memory);
			}
			memory.Position = 0;
			var contentType = "application/pdf";
			var fileName = Path.GetFileName(path);
			return File(memory, contentType, fileName);
		}
	}
}
