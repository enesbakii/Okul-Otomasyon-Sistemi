using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.WebUI.Extensions;
using NsSchool.WebUI.Models;
using System.Security.Claims;

namespace NsSchool.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;
		private readonly IWebHostEnvironment _environment;
		public AccountController(IUserService userService, IWebHostEnvironment environment)
		{
			_userService = userService;
			_environment = environment;

		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			id = User.GetId();
			var personEditDto = _userService.GetUser(id);
			var viewModel = new AccountEditViewModel()
			{
				Id = personEditDto.Id,
				FirstName = personEditDto.FirstName,
				LastName = personEditDto.LastName,
				Adress = personEditDto.Adress,
				BirthDay = personEditDto.BirthDay,
				Email = personEditDto.Email,
				IdentityNumber = personEditDto.IdentityNumber,
				PhoneNumber = personEditDto.PhoneNumber,
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(AccountEditViewModel formData) 
		{
			if (!ModelState.IsValid)
			{
				return View("Edit",formData);
			}


			var newFileName = "";

			if (formData.ImagePath != null)
			{
				var allowedFileContentType = new string[]
				{
					"image/jpeg","image/jpg","image/png","image/jfif"
				};

				var allowedFileExtensions = new string[]
				{
					".jpg",".jpeg",".png",".jfif"
				};

				var fileContentType = formData.ImagePath.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.ImagePath.FileName);
				var fileExtension = Path.GetExtension(formData.ImagePath.FileName);


				if (!allowedFileContentType.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Lütfen jpg,jpeg,png veya jfif tipinde geçerli dosya yükleyiniz";
					
					return View("Form", formData);
				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;
				if (User.IsPerson() || User.IsAdmin())
				{
					var folderPath = Path.Combine("images", "personel");
					var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
					var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);
					Directory.CreateDirectory(wwwRootFolderPath);

					using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
					{
						formData.ImagePath.CopyTo(fileStream);
					}

				}
				else if (User.IsTeacher())
				{
					var folderPath = Path.Combine("images", "teacher");
					var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
					var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);
					Directory.CreateDirectory(wwwRootFolderPath);

					using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
					{
						formData.ImagePath.CopyTo(fileStream);
					}

				}


				else if (User.IsStudent())
				{
					var folderPath = Path.Combine("images", "student");
					var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
					var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);
					Directory.CreateDirectory(wwwRootFolderPath);

					using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
					{
						formData.ImagePath.CopyTo(fileStream);
					}

				}

			}




			var userEditDto = new UserEditDto()
			{
				Id = formData.Id,
				Adress = formData.Adress.ToUpper(),
				BirthDay = formData.BirthDay,
				
				PhoneNumber = formData.PhoneNumber,
			};
			if (!User.IsStudent())
			{
				userEditDto.Email = formData.Email;
			}
			if (formData.ImagePath!=null)
			{
				userEditDto.ImagePath = newFileName;
				User.AddUpdateClaim("imagePath", userEditDto.ImagePath);
			}

			_userService.EditUser(userEditDto);



			//User.AddUpdateClaim("firstName", userEditDto.FirstName);
			//User.AddUpdateClaim("lastName", userEditDto.LastName);


			var claims = User.GetClaims();

			var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			// bu bilgilerle oturum oluştur ^

			var autProperties = new AuthenticationProperties
			{
				AllowRefresh = true, //  sayfa yenilenince oturum açık kalsın
				ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48)) //oturum 48 saat açık kalsın.
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);
			// oluşturulan bilgi dosyaları ve özelliklerle oturumu aç ^

			return RedirectToAction("index", "dash");

		}
		[HttpGet]
		public IActionResult EditPassword()
		{
			return View();
		}
		public IActionResult EditPassword(PasswordEditViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View();
            }

            var editDto = new PasswordEditDto()
            {
                Id = User.GetId(),
                Password = formData.Password,
            };
            _userService.EditPassword(editDto);
            return RedirectToAction("logout","auth");
		}
	}
}
