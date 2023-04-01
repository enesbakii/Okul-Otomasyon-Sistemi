using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Admin.Models;
using System.Diagnostics;

namespace NsSchool.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin,Person")]
	public class StudentController : Controller
	{
		private readonly IStudentService _studentService;
		private readonly IParentService _parentService;
		private readonly IWebHostEnvironment _environment;
		public StudentController(IStudentService studentService, IParentService parentService, IWebHostEnvironment enviroment)
		{
			_studentService = studentService;
			_parentService = parentService;
			_environment = enviroment;
		}

		public IActionResult List()
		{
			var studentDto = _studentService.GetAll();
			var viewModel = studentDto.Select(x => new StudentListViewModel()
			{
				Id = x.Id,
				ClassRoom = x.ClassRoom,
				ImagePath = x.ImagePath,
				ParentFirstName = x.ParentFirstName,
				ParentLastName = x.ParentLastName,
				ParentIdentity = x.ParentIdentity,
				StdentFirstName = x.StdentFirstName,
				StudentLastName = x.StudentLastName,
				StudentIdentity = x.StudentIdentity,
				StudentUserName = x.StudentUserName,
				StudentPassword = x.StudentPassword,
				Grade = x.Grade,

			}).ToList();
			return View(viewModel);
		}
		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Add(StudentAddViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}


			var newFileName = "";

			if (formData.StudentImagePath != null)
			{
				var allowedFileContentType = new string[]
			   {
					"image/jpeg","image/jpg","image/png","image/jfif"
			   };

				var allowedFileExtensions = new string[]
				{
					".jpg",".jpeg",".png",".jfif"
				};

				var fileContentType = formData.StudentImagePath.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.StudentImagePath.FileName);
				var fileExtension = Path.GetExtension(formData.StudentImagePath.FileName);


				if (!allowedFileContentType.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Lütfen jpg,jpeg,png veya jfif tipinde geçerli dosya yükleyiniz";
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("images", "student");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.StudentImagePath.CopyTo(fileStream);
				}
			}




			var studentParentDto = new StudentParentDto()
			{
				StudentIdentityPerson = formData.StudentIdentityPerson,
				StundetFirstName = formData.StundetFirstName,
				StudentLastName = formData.StudentLastName,
				StudentAdress = formData.StudentAdress,
				StudentGender = formData.StudentGender,
				StudentBirthPlace = formData.StudentBirthPlace,
				StudentGrade = formData.StudentGrade,
				StudentNumber = formData.StudentNumber,
				StudentPhoneNumber = formData.StudentPhoneNumber,
				StundetBirthDay = formData.StundetBirthDay,
				AreaInformation = formData.AreaInformation,
				ClassRoom = formData.ClassRoom,
				ParentAdress = formData.ParentAdress,
				ParentGender = formData.ParentGender,
				ParentBirthDay = formData.ParentBirthDay,
				ParentBirthPlace = formData.ParentBirthPlace,
				ParentEmail = formData.ParentEmail,
				ParentFirstName = formData.ParentFirstName,
				ParentIdentity = formData.ParentIdentity,
				ParentLastName = formData.ParentLastName,
				ParentJob = formData.ParentJob,
				ParentPhoneNumber = formData.ParentPhoneNumber,
				StudentImagePath = newFileName,



			};


			var responceParent = _parentService.AddParent(studentParentDto);
			if (responceParent.IsSucceed)
			{
				var parentDto = _parentService.GetByParentIdentity(studentParentDto.ParentIdentity);

				var parentId = parentDto.ParentId;
				studentParentDto.ParentId = parentId;

				var responceStudent = _studentService.AddStudent(studentParentDto);



				if (responceStudent.IsSucceed)
				{

					return RedirectToAction("list");
				}
				else
				{
					ViewBag.ErrorMessage2 = responceStudent.Message;
				}
			}
			else
			{
				var dto = _parentService.GetByParentIdentity(formData.ParentIdentity);

				studentParentDto.ParentId = dto.ParentId;

				var responceStudent = _studentService.AddStudent(studentParentDto);
				if (responceStudent.IsSucceed)
				{
					return RedirectToAction("list");
				}
				else
				{
					ViewBag.ErrorMessage2 = responceStudent.Message;
				}
			}


			ViewBag.ErrorMessage1 = responceParent.Message;

			return View(formData);



		}
		public IActionResult StudentDetail(int id)
		{
			var dto = _studentService.GetById(id);
			var viewModel = new StudentDetailViewModel()
			{
				Id = dto.Id,
				AreaInformation = dto.AreaInformation,
				ClassRoom = dto.ClassRoom,
				ParentAdress = dto.ParentAdress,
				ParentBirthDay = dto.ParentBirthDay,
				ParentBirthPlace = dto.ParentBirthPlace,
				ParentEmail = dto.ParentEmail,
				ParentFirstName = dto.ParentFirstName,
				ParentIdentity = dto.ParentIdentity,
				ParentJob = dto.ParentJob,
				ParentLastName = dto.ParentLastName,
				ParentPassword = dto.ParentPassword,
				ParentPhoneNumber = dto.ParentPhoneNumber,
				ParentUserName = dto.ParentUserName,
				StudentAdress = dto.StudentAdress,
				StudentGrade = dto.StudentGrade,
				StudentBirthPlace = dto.StudentBirthPlace,
				StudentIdentityPerson = dto.StudentIdentityPerson,
				StudentLastName = dto.StudentLastName,
				StudentNumber = dto.StudentNumber,
				StudentPassword = dto.StudentPassword,
				StudentPhoneNumber = dto.StudentPhoneNumber,
				StudentUserName = dto.StudentUserName,
				StundetBirthDay = dto.StundetBirthDay,
				StundetFirstName = dto.StundetFirstName

			};

			return View(viewModel);
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var dto = _studentService.GetByStudent(id);
			var viewModel = new StudentEditViewModel()
			{
				Id = dto.Id,
				AreaInformation = dto.AreaInformation,
				ClassRoom = dto.ClassRoom,
				ParentAdress = dto.ParentAdress,
				ParentBirthDay = dto.ParentBirthDay,
				ParentBirthPlace = dto.ParentBirthPlace,
				ParentEmail = dto.ParentEmail,
				ParentFirstName = dto.ParentFirstName,
				ParentIdentity = dto.ParentIdentity,
				ParentJob = dto.ParentJob,
				ParentLastName = dto.ParentLastName,
				ParentPhoneNumber = dto.ParentPhoneNumber,
				ParentUserName = dto.ParentUserName,
				StudentAdress = dto.StudentAdress,
				StudentBirthPlace = dto.StudentBirthPlace,
				StudentIdentityPerson = dto.StudentIdentityPerson,
				StudentLastName = dto.StudentLastName,
				StudentNumber = dto.StudentNumber,
				StudentPhoneNumber = dto.StudentPhoneNumber,
				StudentUserName = dto.StudentUserName,
				StundetBirthDay = dto.StundetBirthDay,
				StundetFirstName = dto.StundetFirstName,

				ParentGender = dto.ParentGender,
				StudentGender = dto.StudentGender,
				ParentId = dto.ParentId,
			};

			return View(viewModel);
		}
		[HttpPost]
		public IActionResult Edit(StudentEditViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}


			var newFileName = "";

			if (formData.StudentImagePath != null)
			{
				var allowedFileContentType = new string[]
			   {
					"image/jpeg","image/jpg","image/png","image/jfif"
			   };

				var allowedFileExtensions = new string[]
				{
					".jpg",".jpeg",".png",".jfif"
				};

				var fileContentType = formData.StudentImagePath.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.StudentImagePath.FileName);
				var fileExtension = Path.GetExtension(formData.StudentImagePath.FileName);


				if (!allowedFileContentType.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Lütfen jpg,jpeg,png veya jfif tipinde geçerli dosya yükleyiniz";
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("images", "student");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.StudentImagePath.CopyTo(fileStream);
				}
			}

			var studentDto = new StudentEditDto()
			{
				Id = formData.Id,
				AreaInformation = formData.AreaInformation,
				ClassRoom = formData.ClassRoom,
				ParentAdress = formData.ParentAdress,
				ParentBirthDay = formData.ParentBirthDay,
				ParentBirthPlace = formData.ParentBirthPlace,
				ParentEmail = formData.ParentEmail,
				ParentFirstName = formData.ParentFirstName,
				ParentIdentity = formData.ParentIdentity,
				ParentJob = formData.ParentJob,
				ParentLastName = formData.ParentLastName,
				ParentPassword = formData.ParentPassword,
				ParentPhoneNumber = formData.ParentPhoneNumber,
				ParentUserName = formData.ParentUserName,
				StudentAdress = formData.StudentAdress,
				StudentBirthPlace = formData.StudentBirthPlace,
				StudentIdentityPerson = formData.StudentIdentityPerson,
				StudentLastName = formData.StudentLastName,
				StudentNumber = formData.StudentNumber,
				StudentPassword = formData.StudentPassword,
				StudentPhoneNumber = formData.StudentPhoneNumber,
				StudentUserName = formData.StudentUserName,
				StundetBirthDay = formData.StundetBirthDay,
				StundetFirstName = formData.StundetFirstName,
				ParentId = formData.ParentId,
				ParentGender = formData.ParentGender,
				StudentGender = formData.StudentGender,
				StudentGrade = formData.StudentGrade

			};
			if (formData.StudentImagePath != null)
			{
				studentDto.StudentImagePath = newFileName;
			}
			var responceParent = _parentService.EditParent(studentDto);

			var responceStudent = _studentService.EditStudent(studentDto);
			if (!responceStudent.IsSucceed || !responceParent.IsSucceed)
			{
				ViewBag.ErrorMessage = responceStudent.Message;
				ViewBag.ErrorMessage = responceParent.Message;
				return View(formData);

			}
			else
			{
				return RedirectToAction("List");
			}


		}
		public IActionResult Delete(int id)
		{
			_studentService.DeleteStudent(id);
			return RedirectToAction("list");
		}
	}

}
