using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
	public interface IProductService
	{
		ServiceMessage AddProduct(ProductDto productDto);
		List<ProductDto> GetAllProducts();
		void Delete(int id);
		ProductDto GetbyId(int id);
		void EditProduct(ProductDto productDto);
		List<ProductDto> GetProductGetByCategoryId(int? categoryId);

	}
}
