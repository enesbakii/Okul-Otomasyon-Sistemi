using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class CartDetailDto
	{
		public int ProductId { get; set; }
		public int UserId { get; set; }
		public int Count { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
