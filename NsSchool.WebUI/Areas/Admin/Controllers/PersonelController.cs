using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Admin.Models;
using NsSchool.WebUI.Extensions;
using System.Security.Claims;

namespace NsSchool.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
	public class PersonelController : Controller
	{
		private readonly IPersonService _personService;
		private readonly IWebHostEnvironment _environment;
		public PersonelController(IPersonService personService, IWebHostEnvironment environment)
		{
			_personService = personService;
			_environment = environment;
		}
		public IActionResult List()
		{
			var personListDto = _personService.GetPersonList();

			

			var viewModel = personListDto.Select(x=> new PersonListViewModel()
			{
				BirthDay= x.BirthDay,
				Branch=x.Branch,
				Email=x.Email,
				FirstName= x.FirstName,
				LastName= x.LastName,
				Gender=x.Gender,
				IdentityPerson=x.IdentityPerson,
				ImagePath=x.ImagePath,
				Password=x.Password,
				UserName=x.UserName,
				UserType=x.UserType,
				Id=x.Id,
			}).ToList();
			

			return View(viewModel);
		}
		[HttpGet]
		public IActionResult AddPerson()
		{
			return View();
		}
		public IActionResult AddPerson(PersonelAddViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}

			

			var newFileName = "";

			if (formData.File!=null)
			{
				var allowedFileContentType = new string[]
			   {
					"image/jpeg","image/jpg","image/png","image/jfif"
			   };

				var allowedFileExtensions = new string[]
				{
					".jpg",".jpeg",".png",".jfif"
				};

				var fileContentType = formData.File.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.File.FileName);
				var fileExtension = Path.GetExtension(formData.File.FileName);


				if (!allowedFileContentType.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Lütfen jpg,jpeg,png veya jfif tipinde geçerli dosya yükleyiniz";
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;
				
				var folderPath = Path.Combine("images", "personel");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.File.CopyTo(fileStream);
				}
			}




			var personAddDto = new PersonAddDto()
			{
				Adress = formData.Adress,
				BirthDay = formData.BirthDay,
				BirthPlace = formData.BirthPlace,
				Email = formData.Email,
				FirstName = formData.FirstName,
				Id = formData.Id,
				IdentityPerson = formData.IdentityPerson,
				Lastname = formData.Lastname,
				PhoneNumber = formData.PhoneNumber,
				Gender = formData.Gender,
				ImagePath = newFileName
			};

			var responce = _personService.AddPerson(personAddDto);
			if (responce.IsSucceed)
			{
				return RedirectToAction("List", "Personel");
			}

            ViewBag.ErrorMessage = responce.Message;
            return View(formData);


           
		}
		[HttpGet]
		public IActionResult EditPerson(int id)
		{
			var personEditDto = _personService.GetPerson(id);
			var viewModel = new PersonelEditViewModel()
			{
				UserName = personEditDto.UserName,
				PhoneNumber = personEditDto.PhoneNumber,
				Gender = personEditDto.Gender,
				LastName = personEditDto.LastName,
				Adress = personEditDto.Adress,
				BirthDay = personEditDto.BirthDay,
				BirthPlace = personEditDto.BirthPlace,
				Email = personEditDto.Email,
				FirstName = personEditDto.FistName,
				Id = personEditDto.Id,
				IdentityPerson = personEditDto.IdentityPerson,
				
			};
			ViewBag.ImagePath = personEditDto.ImagePath;

			return View(viewModel);
		}
		[HttpPost]
		public IActionResult EditPerson(PersonelEditViewModel formData)
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

					return View("Form", formData);
				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("images", "personel");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.ImagePath.CopyTo(fileStream);
				}

			}

			var personelEditDto = new PersonEditDto()
			{
				Adress = formData.Adress,
				BirthDay = formData.BirthDay,
				BirthPlace = formData.BirthPlace.Trim(),
				Email = formData.Email.Trim(),
				FistName = formData.FirstName.Trim(),
				Gender = formData.Gender,
				Id = formData.Id,
				IdentityPerson = formData.IdentityPerson.Trim(),
				LastName = formData.LastName.Trim(),
				PhoneNumber = formData.PhoneNumber.Trim(),
				UserName = formData.UserName.Trim(),
				

			};
			if (formData.ImagePath!=null)
			{
				personelEditDto.ImagePath = newFileName;
			}
			if (formData.Password!=null)
			{
				personelEditDto.Password = formData.Password;
			}

			var responce= _personService.EditPerson(personelEditDto);
			if (responce.IsSucceed)
			{
				
				return RedirectToAction("List", "Personel");

			}
			else
			{
				ViewBag.ErrorEditMessage = responce.Message;
				return View(formData);
			}
			
			

		}

		public IActionResult Delete(int id)
		{
			_personService.DeletePerson(id);
			return RedirectToAction("list", "personel");
		}
	}
}
