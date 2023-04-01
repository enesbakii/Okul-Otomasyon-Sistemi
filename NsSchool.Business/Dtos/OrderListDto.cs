using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
}
