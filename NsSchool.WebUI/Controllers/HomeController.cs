using Microsoft.AspNetCore.Mvc;
using NsSchool.WebUI.Extensions;

namespace NsSchool.WebUI.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			if (User.IsLogged())
			{
				return RedirectToAction("Index", "Dash");
			}
			return View();
		}
	}
}
