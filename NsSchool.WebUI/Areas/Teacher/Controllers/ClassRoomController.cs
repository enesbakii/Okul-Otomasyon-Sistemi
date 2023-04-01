using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Teacher.Models;
using NsSchool.WebUI.Extensions;

namespace NsSchool.WebUI.Areas.Teacher.Controllers
{
	[Area("Teacher")]
	[Authorize(Roles ="Teacher")]
	public class ClassRoomController : Controller
	{
		private readonly IClassRoomTeacherService _classRoomTeacherService;
		public ClassRoomController(IClassRoomTeacherService classRoomTeacherService)
		{
			_classRoomTeacherService = classRoomTeacherService;
		}

		public IActionResult List()
		{
			var dto = _classRoomTeacherService.ListStudent(User.GetId());
		
			var viewModel =dto.Select(x=> new ClassRoomStudentViewModel()
			{
				
				ClassRoom=x.ClassRoom,
				FirstName=x.Students.FirstName,
				LastName=x.Students.LastName,
			}).ToList();
		
			return View(viewModel);
		}
	}
}
