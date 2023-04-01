using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Admin.Models;

namespace NsSchool.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly ICartServiceDetail _cartService;
        public OrderController(ICartServiceDetail cartService)
        {
            _cartService= cartService;
        }
        public IActionResult List()
        {
            var dto = _cartService.GettAll();
            var viewModel = dto.Select(x=> new OrderListViewModel()
            {
                Id= x.Id,
                CategoryName=x.CategoryName,
                Count= x.Count,
                FirstName= x.FistName,
                LastName= x.LastName,
                ProductName= x.ProductName,
                UnitPrice=x.UnitPrice,
            }).ToList();
            return View(viewModel);
        }
    }
}
