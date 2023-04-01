﻿using Microsoft.AspNetCore.Mvc;

namespace NsSchool.WebUI.Controllers
{
	public class ErrorController : Controller
	{

		public IActionResult Error(int code)
		{
			ViewBag.ErrorCode = code;
			return View();
		}
	}
}

