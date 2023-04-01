using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Parent.Models;

namespace NsSchool.WebUI.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles ="Parent")]
    public class ShopController : Controller
    {
		private readonly IProductService _productService;
		public ShopController(IProductService productService)
		{
			_productService = productService;
		}
		public IActionResult List(int? id = null)
		{

			var dto = _productService.GetProductGetByCategoryId(id);
			var viewModel = dto.Select(x => new ProductListViewModel()
			{
				Name = x.Name,
				CategoryId = x.CategoryId,
				CategoryName = x.CategoryName,
				Discription = x.Discription,
				Id = x.Id,
				ImagePath = x.ImagePath,
				UnitPrice = x.UnitPrice
			}).ToList();
			return View(viewModel);
		}
		public IActionResult Detail(int id)
		{
			var dto = _productService.GetbyId(id);
			var viewModel = new ProductListViewModel()
			{
				CategoryId = dto.CategoryId,
				CategoryName = dto.CategoryName,
				Discription = dto.Discription,
				Id = dto.Id,
				ImagePath = dto.ImagePath,
				Name = dto.Name,
				UnitPrice = dto.UnitPrice
			};
			return View(viewModel);
		}
	
	}
}
