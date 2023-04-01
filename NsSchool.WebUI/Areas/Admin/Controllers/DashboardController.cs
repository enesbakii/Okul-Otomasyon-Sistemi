using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NsSchool.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin,Person")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
