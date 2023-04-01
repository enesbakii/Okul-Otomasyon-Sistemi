using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
	public class AnnouncementEditDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Discrpiton { get; set; }
		public string? Path { get; set; }

	}

}
