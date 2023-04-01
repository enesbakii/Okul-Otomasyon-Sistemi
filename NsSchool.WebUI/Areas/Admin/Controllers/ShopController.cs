using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Admin.Models;
using System.Dynamic;

namespace NsSchool.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ShopController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IWebHostEnvironment _environment;
		private readonly IProductService _productService;
		public ShopController(ICategoryService categoryService, IWebHostEnvironment environment, IProductService productService)

		{
			_categoryService = categoryService;
			_environment = environment;
			_productService = productService;
		}

		public IActionResult Index()
		{
			var categoryDto = _categoryService.GetCategories();
			var categoryModel = categoryDto.Select(x => new CategoryListViewModel()
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();

			var productDto = _productService.GetAllProducts();

			var productModel = productDto.Select(x=> new ProductListViewModel()
			{
				ProductId=x.Id,
				ProductName=x.Name,
				ProductDescription=x.Discription,
				ProductPrice=x.UnitPrice,
				CategoryId=x.Id,
				 CategoryName=x.CategoryName,
			}).ToList() ;

			var viewModel = new ViewModel();
			viewModel.Products = productModel;
			viewModel.Categories = categoryModel;

			return View(viewModel);

		}
		[HttpGet]
		public IActionResult AddCategory()
		{
			ViewBag.Categories = _categoryService.GetCategories();
			return View();
		}
		[HttpPost]
		public IActionResult AddCategory(CategoryFormViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}

			var dto = new CategoryDto()
			{
				Name = formData.Name,
			};
			var responce = _categoryService.AddCategory(dto);

			if (!responce.IsSucceed)
			{
				ViewBag.ErrorMessage = responce.Message;
				return View(formData);

			}

			return RedirectToAction("Index");



		}
		[HttpGet]
		public IActionResult CategoryEdit(int id)
		{
			var categoryDto = _categoryService.GetById(id);
			var viewModel = new CategoryFormViewModel()
			{
				Name = categoryDto.Name,
				Id = categoryDto.Id
			};
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult CategoryEdit(CategoryFormViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}
			var categoryDto = new CategoryDto()
			{
				Name = formData.Name,
				Id = formData.Id
			};
			_categoryService.EditCategory(categoryDto);
			return RedirectToAction("Index");
		}
		public IActionResult CategoryDelete(int id)
		{
			_categoryService.DeleteCategory(id);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult AddProduct()
		{
			if (_categoryService.GetCategories().Any())
			{
				ViewBag.Categories = _categoryService.GetCategories();
				return View();
			}
			ViewBag.ErrorCategory = "Kategori eklemeniz gerekmektedir";
			return View();
		}
		public IActionResult AddProduct(ProductFormViewModel formData)
		{
			
			if (!ModelState.IsValid)
			{
			
				ViewBag.Categories = _categoryService.GetCategories();
				return View(formData);
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
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("images", "product");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.ImagePath.CopyTo(fileStream);
				}
			}
		
			var productDto = new ProductDto()
			{
				Name = formData.Name,
				CategoryId = formData.CategoryId,
				Discription = formData.Discription,
				ImagePath = newFileName,
				UnitPrice = formData.UnitPrice,
			};

			var responce =_productService.AddProduct(productDto);
			if (!responce.IsSucceed)
			{
				ViewBag.Categories = _categoryService.GetCategories();
				ViewBag.ErrorMessage = responce.Message;
				return View(formData);
			}
			
			return RedirectToAction("index");
		}
		public IActionResult ProductDelete(int id)
		{
			_productService.Delete(id);
			return RedirectToAction("index");
		}
		[HttpGet]
		public IActionResult ProductEdit(int id)
		{
			ViewBag.Categories = _categoryService.GetCategories();
			var productDto = _productService.GetbyId(id);
			var viewModel = new ProductEditViewModel()
			{
				Id = productDto.Id,
				CategoryId = productDto.CategoryId,
				Discription = productDto.Discription,
				Name = productDto.Name,
				UnitPrice = productDto.UnitPrice,
			};
			ViewBag.ImagePath = productDto.ImagePath;
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult ProductEdit(ProductEditViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Categories = _categoryService.GetCategories();
				return View(formData);
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
					return View(formData);


				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("images", "product");
				var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
				var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

				Directory.CreateDirectory(wwwRootFolderPath);

				using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
				{
					formData.ImagePath.CopyTo(fileStream);
				}
			}



			var productDto = new ProductDto()
			{
				Id = formData.Id,
				CategoryId = formData.CategoryId,
				Discription = formData.Discription,
				Name = formData.Name,
				UnitPrice = formData.UnitPrice
			};
			if (formData.ImagePath!=null)
			{
				productDto.ImagePath =newFileName;
			}

			_productService.EditProduct(productDto);
			return RedirectToAction("index");
		}
	}
}
