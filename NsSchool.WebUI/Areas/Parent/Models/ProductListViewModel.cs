namespace NsSchool.WebUI.Areas.Parent.Models
{
	public class ProductListViewModel
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string Name { get; set; }
		public string Discription { get; set; }
		public string ImagePath { get; set; }
		public decimal UnitPrice { get; set; }
		public int Count { get; set; }
	}
}
