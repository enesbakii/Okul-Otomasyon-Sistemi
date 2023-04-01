using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string Name { get; set; }
		public string Discription { get; set; }
		public string ImagePath { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
