using NsSchool.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
	public interface ICartServiceDetail
	{
		void AddCart(CartDetailDto cartDetailDto);
		List<CartDetailListDto> ListCart(int? id);
		void DeleteCart(int id);
		void BuyCart(CartDetailDto cartDetailDto);

		List<OrderListDto> GettAll();
	}
}
