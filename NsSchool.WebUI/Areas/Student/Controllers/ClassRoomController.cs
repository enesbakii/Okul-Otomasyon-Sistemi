using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Student.Models;
using NsSchool.WebUI.Extensions;

namespace NsSchool.WebUI.Areas.Student.Controllers
{
	[Area("Student")]
	[Authorize(Roles ="Student")]
	public class ClassRoomController : Controller
	{
		private readonly IClassRoomStudentService _classRoomStudentService;
		public ClassRoomController(IClassRoomStudentService classRoomStudentService)
		{
			_classRoomStudentService= classRoomStudentService;
		}
		public IActionResult List()
		{
			var dto =_classRoomStudentService.ListStudent(User.GetId());
			var viewModel = dto.Select(x=> new ClassRoomStudentViewModel()
			{
				FirstName= x.FirstName,
				ClassRoom=x.ClassRoom,
				LastName= x.LastName,
				
			}).ToList();
			return View(viewModel);
		}
	}
}
