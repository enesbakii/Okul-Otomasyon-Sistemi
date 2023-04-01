namespace NsSchool.WebUI.Areas.Admin.Models
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public decimal TotalPrice { get { return UnitPrice * Count; } }
    }
}
