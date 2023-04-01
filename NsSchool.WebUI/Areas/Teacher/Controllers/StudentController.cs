using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Admin.Models;
using NsSchool.WebUI.Areas.Teacher.Models;
using StudentDetailViewModel = NsSchool.WebUI.Areas.Teacher.Models.StudentDetailViewModel;
using StudentListViewModel = NsSchool.WebUI.Areas.Teacher.Models.StudentListViewModel;

namespace NsSchool.WebUI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles ="Teacher")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
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
                StudentPassword = x.StudentPassword

            }).ToList();
            return View(viewModel);
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
	}
}
