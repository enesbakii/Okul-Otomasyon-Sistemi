using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class CartDetailEntity:BaseEntity
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int Count { get; set; }

		//relational property
		public ProductEntity Product { get; set; }
		public UserEntity User { get; set; }
	}
}
