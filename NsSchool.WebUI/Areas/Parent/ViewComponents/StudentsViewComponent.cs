using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Parent.Models;
using NsSchool.WebUI.Extensions;

namespace NsSchool.WebUI.Areas.Parent.ViewComponents
{
	
	public class StudentsViewComponent :ViewComponent
	{
		private readonly IParentService _parentService;
		public StudentsViewComponent(IParentService parentService)
		{
			_parentService = parentService;
		}
		public IViewComponentResult Invoke(int id)
		{
			id=UserClaimsPrincipal.GetId();
			var dto = _parentService.GetStudents(id);
			var viewModel = dto.Select(x => new StudentListViewModel()
			{
				FirstName = x.FirstName,
				LastName = x.LastName,
			}).ToList();
			return View(viewModel);
		}
	}
}
