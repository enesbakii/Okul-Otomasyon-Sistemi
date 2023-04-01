using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.WebUI.Models;
using System.Security.Claims;

namespace NsSchool.WebUI.Controllers
{
	public class AuthController : Controller
	{
		private readonly IUserService _userService;

		public AuthController(IUserService userService)
		{
			_userService= userService;
		}

		[Route("giris-yap")]
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Login(LoginViewModel formData)
		{
			if (!ModelState.IsValid)
			{
                TempData["LoginMassage"] = "* Kullanıcı adı veya şifreyi boş geçilemez.";
                return RedirectToAction("index");
			}


			var loginDto = new LoginDto()
			{
				UserName = formData.UserName,
				Password = formData.Password,
			};

			var userDto =_userService.LoginPerson(loginDto);

			if (userDto is null)
			{
                TempData["LoginMassage"] = "* Kullanıcı adı veya şifreyi hatalı girdiniz.";

				return RedirectToAction("index", "auth");
			}

			var claims = new List<Claim>();
			claims.Add(new Claim("id", userDto.Id.ToString()));
			claims.Add(new Claim("firstName", userDto.FirstName));
			claims.Add(new Claim("lastName", userDto.LastName));
			claims.Add(new Claim("userName", userDto.UserName));
			if (userDto.ImagePath!=null)
			{
				claims.Add(new Claim("imagePath", userDto.ImagePath));
			}
			
			claims.Add(new Claim("userType", userDto.UserType.ToString()));

			claims.Add(new Claim(ClaimTypes.Role, userDto.UserType.ToString()));


			var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			// bu bilgilerle oturum oluştur ^

			var autProperties = new AuthenticationProperties
			{
				AllowRefresh = true, //  sayfa yenilenince oturum açık kalsın
				ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)) //oturum 48 saat açık kalsın.
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);
			// oluşturulan bilgi dosyaları ve özelliklerle oturumu aç ^


			return RedirectToAction("Index", "Dash");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Auth");
		}

	}
}
