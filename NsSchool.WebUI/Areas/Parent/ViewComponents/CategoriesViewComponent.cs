using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Parent.Models;

namespace NsSchool.WebUI.Areas.Parent.ViewComponents
{
	public class CategoriesViewComponent : ViewComponent
	{
		private readonly ICategoryService _categoryService;
		public CategoriesViewComponent(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		public IViewComponentResult Invoke()
		{
			
			var dto = _categoryService.GetCategories();
			var viewModel = dto.Select(x => new CategoryListViewModel()
			{
				Id = x.Id,
				Name = x.Name
			}).ToList();
			return View(viewModel);
		}
	}
}
