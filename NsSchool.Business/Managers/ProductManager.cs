using Microsoft.EntityFrameworkCore;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.Business.Types;
using NsSchool.Data.Entites;
using NsSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Managers
{
	public class ProductManager:IProductService
	{
		private readonly IRepository<ProductEntity> _productService;
		public ProductManager(IRepository<ProductEntity> productService)
		{
			_productService = productService;
		}

		public ServiceMessage AddProduct(ProductDto productDto)
		{
			var hasProduct = _productService.GetAll(x=>x.Name==productDto.Name);
			if (hasProduct.Any())
			{
				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu ürün zaten mevcut"
				};
			}
			var entity = new ProductEntity()
			{
				Name = productDto.Name,
				CategoryId = productDto.CategoryId,
				ImagePath = productDto.ImagePath,
				Discription = productDto.Discription,
				Price = productDto.UnitPrice
			};
			_productService.Add(entity);
			return new ServiceMessage()
			{
				IsSucceed = true
			};
		}

		public void Delete(int id)
		{
			_productService.Delete(id);
		}


		public void EditProduct(ProductDto productDto)
		{
			var entity = _productService.GetById(productDto.Id);
			entity.Name= productDto.Name;
			entity.CategoryId = productDto.CategoryId;
			entity.Discription = productDto.Discription;
			entity.Price = productDto.UnitPrice;
			if (productDto.ImagePath!=null)
			{
				entity.ImagePath = productDto.ImagePath;
			}
			_productService.Update(entity);
		}

		public List<ProductDto> GetAllProducts()
		{
			var entities = _productService.GetAll().OrderBy(x => x.Name);
			var productDto = entities
				.Include(x=>x.Category)
				.Select(x=> new ProductDto()
				{
					Name = x.Name,
					CategoryId = x.CategoryId,
					ImagePath = x.ImagePath,
					Discription = x.Discription,
					Id = x.Id,
					UnitPrice = x.Price,
					CategoryName = x.Category.Name,
				}).ToList();
			return productDto;
		}

		public ProductDto GetbyId(int id)
		{
			var entity=_productService.GetById(id);
			var productDto = new ProductDto()
			{
				Id = entity.Id,
				Name = entity.Name,
				CategoryId = entity.CategoryId,
				Discription = entity.Discription,
				ImagePath = entity.ImagePath,
				UnitPrice = entity.Price,
			};
			return productDto;
		}

		public List<ProductDto> GetProductGetByCategoryId(int? categoryId)
		{
			if (categoryId.HasValue)
			{
				var productEntity = _productService.GetAll(x => x.CategoryId == categoryId);
				var productDto = productEntity.Select(x => new ProductDto()
				{
					CategoryId = x.CategoryId,
					CategoryName = x.Category.Name,
					Name = x.Name,
					Discription = x.Discription,
					Id = x.Id,
					ImagePath = x.ImagePath,
					UnitPrice = x.Price,
				}).ToList();
				return productDto;
			}
			return GetAllProducts();
		}
	}
}
