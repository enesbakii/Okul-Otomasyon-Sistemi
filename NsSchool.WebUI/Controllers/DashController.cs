using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Models;

namespace NsSchool.WebUI.Controllers
{
	[Authorize]
	public class DashController : Controller
	{
		private readonly IAnnouncementService _announcementService;
		public DashController(IAnnouncementService announcementService)
		{
			_announcementService=announcementService;
		}
		public IActionResult Index()
		{
			var announcementDto = _announcementService.GetAnnouncementList();
			var viewModel = announcementDto.Select(x=> new AnnouncementViewModel()
			{
				CreatedDate=x.CreatedDate,
				Discripton=x.Discription,
				Id=x.Id,
				ModifiedDate=x.ModifiedDate,
				Path=x.Path,
				Title=x.Title.ToUpper(),
			}).Take(5).ToList();
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
