using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Parent.Models;
using NsSchool.WebUI.Extensions;

namespace NsSchool.WebUI.Areas.Parent.Controllers
{

	[Area("Parent")]
	[Authorize(Roles = "Parent")]
	public class CartController : Controller
	{
		private readonly ICartServiceDetail _cartDetailService;
		public CartController(ICartServiceDetail cartDetailService)
		{
			_cartDetailService = cartDetailService;
		}

		[HttpPost]
		public IActionResult AddCart(int id,CartDetailViewModel formData)
		{
			var dto = new CartDetailDto()
			{
				Count = formData.Count,
				ProductId = id,
				UnitPrice = formData.UnitPrice,
				UserId = User.GetId()
			};
			_cartDetailService.AddCart(dto);
			return RedirectToAction("list","Shop");
		}
		public IActionResult ListCart(int? id=null)
		{
			var dto =_cartDetailService.ListCart(id);
			var viewModel = dto.Select(x => new CartDetailListViewModel()
			{
				ImagePath = x.ImagePath,
				Count = x.Count,
				ProductName = x.ProductName,
				UnitPrice = x.UnitPrice,
				Id=x.Id
				
			}).ToList();

			
			return View(viewModel);
		}
		public IActionResult DeleteCart(int id) 
		{ 
			int userId = User.GetId();
			_cartDetailService.DeleteCart(id);
			return RedirectToAction($"ListCart", new {@id=userId });
		}
		public IActionResult Buy()
		{
			var cartdto = new CartDetailDto()
			{
				UserId = User.GetId(),

			};
			_cartDetailService.BuyCart(cartdto);
			return RedirectToAction("list","shop");
		}
	}
}
