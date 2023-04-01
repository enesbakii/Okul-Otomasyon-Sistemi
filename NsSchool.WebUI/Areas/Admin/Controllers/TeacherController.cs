using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.Data.Enums;
using NsSchool.WebUI.Areas.Admin.Models;
using NsSchool.WebUI.Areas.Enums;

namespace NsSchool.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin,Person")]
	public class TeacherController : Controller
	{
		private readonly IWebHostEnvironment _environment;
		private readonly ITeacherService _teacherService;
		public TeacherController(IWebHostEnvironment environment, ITeacherService teacherService)
		{
			_environment= environment;
			_teacherService= teacherService;
		}

        public IActionResult List()
		{
			var dto =_teacherService.GetTeacherList();

			var viewModel = dto.Select(x => new TeacherListViewModel()
			{
				UserType = x.UserType,
				UserName = x.UserName,
				PhoneNumber = x.PhoneNumber,
				Password = x.Password,
				LastName = x.LastName,
				ImagePath = x.ImagePath,
				IdentityPerson = x.IdentityPerson,
				FirstName = x.FirstName,
				Branch = x.Branch,
				Email = x.Email,
				Id = x.Id
			}).ToList();

			return View(viewModel);
		}
		[HttpGet]
		public IActionResult AddTeacher()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddTeacher(TeacherAddViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}

			var newFileName = "";

			if (formData.ImagePath != null)
			{
				var allowedFileContentType = new string[]
			   {
					"image/jpeg","image/jpg","image/png","image/jfif"
			   };

				var allowedFileExtensions = new string[]
				{
					".jpg",".jpeg",".png",".jfif"
				};

				var fileContentType = formData.ImagePath.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.ImagePath.FileName);
				var fileExtension = Path.GetExtension(formData.ImagePath.FileName);


				if (!allowedFileContentType.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Lütfen jpg,jpeg,png veya jfif tipinde geçerli dosya yükleyiniz";
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("images", "teacher");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.ImagePath.CopyTo(fileStream);
				}
			}




			var teacherDto = new TeacherAddDto()
			{
				Adress = formData.Adress,
				BirthDay = formData.BirthDay,
				BirthPlace = formData.BirthPlace,
				Branch = formData.Branch.Value.ToString(),
				Email = formData.Email,
				FirstName = formData.FirstName,
				Gender = formData.Gender,
				IdentityPerson = formData.IdentityPerson,
				LastName = formData.LastName,
				PhoneNumber = formData.PhoneNumber,
				ImagePath=newFileName
			};

			var responce =_teacherService.AddTeacher(teacherDto);
			if (!responce.IsSucceed)
			{
				ViewBag.ErrorMessage = responce.Message;
				return View(formData);
				
			}
			
			return RedirectToAction("list");

		
		}
		[HttpGet]
		public IActionResult EditTeacher(int id)
		{
			var teacherEditDto =_teacherService.GetById(id);
			var viewModel = new TeacherEditViewModel()
			{
				PhoneNumber = teacherEditDto.PhoneNumber,
				IdentityPerson = teacherEditDto.IdentityPerson,
				Adress = teacherEditDto.Adress,
				BirthDay = teacherEditDto.BirthDay,
				LastName = teacherEditDto.LastName,
				FirstName = teacherEditDto.FirstName,
				BirthPlace = teacherEditDto.BirthPlace,
				Branch =(BranchTypeEnum)Enum.Parse(typeof(BranchTypeEnum),teacherEditDto.Branch,true),
				Email = teacherEditDto.Email,
				Gender = teacherEditDto.Gender,
				Id = teacherEditDto.Id,
				UserName= teacherEditDto.UserName,
				
				
			};
			return View(viewModel);
		}
		public IActionResult EditTeacher(TeacherEditViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}
			var newFileName = "";

			if (formData.ImagePath != null)
			{
				var allowedFileContentType = new string[]
			   {
					"image/jpeg","image/jpg","image/png","image/jfif"
			   };

				var allowedFileExtensions = new string[]
				{
					".jpg",".jpeg",".png",".jfif"
				};

				var fileContentType = formData.ImagePath.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.ImagePath.FileName);
				var fileExtension = Path.GetExtension(formData.ImagePath.FileName);


				if (!allowedFileContentType.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Lütfen jpg,jpeg,png veya jfif tipinde geçerli dosya yükleyiniz";
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("images", "teacher");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.ImagePath.CopyTo(fileStream);
				}
			}





			var teacherEditDto = new TeacherEditDto()
			{
				Branch=formData.Branch.Value.ToString(),
				BirthPlace=formData.BirthPlace,
				Adress=formData.Adress,
				BirthDay=formData.BirthDay,
				Email=formData.Email,
				FirstName=formData.FirstName,
				Gender=formData.Gender,
				Id=formData.Id,
				IdentityPerson=formData.IdentityPerson,
				LastName=formData.LastName,
				PhoneNumber=formData.PhoneNumber,
				UserName=formData.IdentityPerson,
				
			};
			if (formData.ImagePath!=null)
			{
				teacherEditDto.ImagePath = newFileName;
			}
			if (formData.Password!=null)
			{
				teacherEditDto.Password = formData.Password;
			}

			var responce =_teacherService.EditTeacher(teacherEditDto);
			if (responce.IsSucceed)
			{
				return RedirectToAction("list");
			}
			else
			{
				ViewBag.ErrorEditMessage = responce.Message;
				return View(formData);
			}
			
		}
		public IActionResult Delete(int id)
		{
			_teacherService.DeleteTeacher(id);
			return RedirectToAction("list");
		}
	}
}
