using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Parent.Models;
using NsSchool.WebUI.Extensions;

namespace NsSchool.WebUI.Areas.Parent.Controllers
{
	[Area("Parent")]
	[Authorize(Roles ="Parent")]
	public class StudentController : Controller
	{
		private readonly IParentService _parentService;
		public StudentController(IParentService parentService)
		{
			_parentService = parentService;
		}

		public IActionResult List()
		{
			var dto = _parentService.GetStudents(User.GetId());
			var viewModel = dto.Select(x => new StudentListViewModel()
			{
				FirstName = x.FirstName,
				LastName = x.LastName,
			}).ToList();
			return View(viewModel);
		}
	}
}
