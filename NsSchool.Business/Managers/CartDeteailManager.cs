using Microsoft.EntityFrameworkCore;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.Data.Entites;
using NsSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Managers
{
	public class CartDeteailManager : ICartServiceDetail
	{
		private readonly IRepository<CartDetailEntity> _cartRepository;
		public CartDeteailManager(IRepository<CartDetailEntity> cartRepository)
		{
			_cartRepository = cartRepository;
		}

		public void AddCart(CartDetailDto cartDetailDto)
		{
			var entity = new CartDetailEntity()
			{
				ProductId = cartDetailDto.ProductId,
				Count = cartDetailDto.Count,
				UserId = cartDetailDto.UserId,

			};
			_cartRepository.Add(entity);
		}

		public void BuyCart(CartDetailDto cartDetailDto)
		{
			var carts = new List<CartDetailEntity>();
			var entity=_cartRepository.GetAll(x => x.UserId ==cartDetailDto.UserId).ToList();
			foreach (var item in entity)
			{
				item.IsDeleted = true;
				item.ModifiedDate= DateTime.Now;
				carts.Add(item);
			}
			_cartRepository.BulkUpdate(carts);
		}

		public void DeleteCart(int id)
		{
			var entity = _cartRepository.GetById(id);
			_cartRepository.HardDelete(entity);
		}

        public List<OrderListDto> GettAll()
        {
            
			var entity =_cartRepository.GetAll()
				.Where(x=>x.IsDeleted==true)
				.Include(x=>x.Product)
				.ThenInclude(x=>x.Category);

			var dto = entity.Select(x=> new OrderListDto()
			{
				Id= x.Id,
				CategoryName=x.Product.Category.Name,
				Count=x.Count,
				FistName=x.User.Parent.FirstName,
				LastName=x.User.Parent.LastName,
				ProductName=x.Product.Name,
				UnitPrice=x.Product.Price
				
			}).ToList();
			return dto;
        }

        public List<CartDetailListDto> ListCart(int? id)
		{
			var entity = _cartRepository.GetAll(x=>x.UserId==id);
			var dto = entity.Where(x=>x.IsDeleted==false).Select(x=> new CartDetailListDto()
			{
				Count= x.Count,
				ProductId= x.ProductId,
				ProductName=x.Product.Name,
				UnitPrice=x.Product.Price,
				UserId=x.UserId,
				ImagePath=x.Product.ImagePath,
				Id=x.Id
			}).ToList();
			return dto;
		}
	}
}
