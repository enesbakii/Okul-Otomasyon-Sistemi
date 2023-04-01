namespace NsSchool.WebUI.Areas.Parent.Models
{
	public class CartDetailListViewModel
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public decimal UnitPrice { get; set; }
		public int Count { get; set; }
		public string ImagePath { get; set; }
		public decimal TotalPrice { get { return UnitPrice * Count; } }

	}
}
