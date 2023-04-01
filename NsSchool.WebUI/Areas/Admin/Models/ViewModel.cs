using NsSchool.Business.Dtos;

namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class ViewModel
	{
		public List<CategoryListViewModel> Categories { get; set; }
		public List<ProductListViewModel> Products { get; set; }
	}
}
