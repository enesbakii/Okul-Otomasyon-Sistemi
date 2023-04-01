using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Admin.Models;

namespace NsSchool.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin,Person")]
	public class AnnouncementController : Controller
	{
		private readonly IAnnouncementService _announcementService;
		private readonly IWebHostEnvironment _environment;
		public AnnouncementController(IAnnouncementService announcementService, IWebHostEnvironment environment)
		{
            _announcementService=announcementService;
			_environment=environment;
        }
		public IActionResult List()
		{



			var announcementDtos = _announcementService.GetAnnouncementList();
			var viewModel = announcementDtos.Select(x => new AnnouncementListViewModel()
			{
				Discripton = x.Discription,
				Id = x.Id,
				Title = x.Title,
				Path = x.Path,
				CreatedDate = x.CreatedDate,
				ModifiedDate=x.ModifiedDate,
				
			}).ToList();
			return View(viewModel);
		}
		[HttpGet]
		public IActionResult AddAnnouncement()
		{


			return View();
		}

		[HttpPost]
		public IActionResult AddAnnouncement(AnnouncementFormViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}


			var newFileName = "";

			if (formData.Path != null)
			{
				var allowedFileContentType = new string[]
			   {
					"application/pdf"
			   };

				var allowedFileExtensions = new string[]
				{
					".pdf"
				};

				var fileContentType = formData.Path.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.Path.FileName);
				var fileExtension = Path.GetExtension(formData.Path.FileName);


				if (!allowedFileContentType.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Lütfen PDF formatında dosya yükleyiniz.";
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("announcement", "announcementPDF");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.Path.CopyTo(fileStream);
				}
			}




			var announcementDto = new AnnouncementDto()
			{
				Title = formData.Title,
				Discription = formData.Discription,
				Path=newFileName

			};
			var responce = _announcementService.AddAnnouncement(announcementDto);
			if (responce.IsSucceed)
			{
				return RedirectToAction("list");
			}

			return View(formData);

		}
		public IActionResult Delete(int id)
		{
			_announcementService.DeleteAnnouncement(id);
			return RedirectToAction("list");
		}
		public IActionResult EditAnnouncement(int id) 
		{
			var announcementEditDto = _announcementService.GetAnnouncement(id);
			var viewModel = new AnnouncementEditViewModel()
			{
				Id = announcementEditDto.Id,
				Title = announcementEditDto.Title,
				Discription = announcementEditDto.Discrpiton,
			};
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult EditAnnouncement(AnnouncementEditViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}


			var newFileName = "";

			if (formData.Path != null)
			{
				var allowedFileContentType = new string[]
			   {
					"application/pdf"
			   };

				var allowedFileExtensions = new string[]
				{
					".pdf"
				};

				var fileContentType = formData.Path.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.Path.FileName);
				var fileExtension = Path.GetExtension(formData.Path.FileName);


				if (!allowedFileContentType.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Lütfen PDF formatında dosya yükleyiniz.";
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("announcement", "announcementPDF");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.Path.CopyTo(fileStream);
				}
			}


			var announcementEditDto = new AnnouncementEditDto()
			{
				Id = formData.Id,
				Discrpiton = formData.Discription,
				Title = formData.Title,
				Path = newFileName
		};


				


			_announcementService.EditAnnouncement(announcementEditDto);

			return RedirectToAction("list");

		}
		public IActionResult Download(string filePath)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "announcement", "announcementPDF" ,filePath);
			
			var memory = new MemoryStream();
			using (var stream = new FileStream(path,FileMode.Open))
			{
				stream.CopyTo(memory);
			}
			memory.Position = 0;
			var contentType = "application/pdf";
			var fileName = Path.GetFileName(path);
			return File(memory,contentType, fileName);
		}
	}
}
