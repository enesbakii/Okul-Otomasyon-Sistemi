namespace NsSchool.WebUI.Areas.Admin.Models
{
	public class ProductListViewModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public decimal ProductPrice { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
	}
}
