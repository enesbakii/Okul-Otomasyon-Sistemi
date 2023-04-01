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
    public class CategoryManager :ICategoryService
    {
        private readonly IRepository<CategoryEntity> _categoryRepository;
        public CategoryManager(IRepository<CategoryEntity> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ServiceMessage AddCategory(CategoryDto categoryDto)
        {
            var hasCategory = _categoryRepository.GetAll(x => x.Name==categoryDto.Name);
            if (hasCategory.Any())
            {
                return new ServiceMessage()
                {
                    IsSucceed = false,
                    Message = "Bu kategroriden zaten mevcut"
                };
            }

            var entity = new CategoryEntity()
            {
                Name = categoryDto.Name.ToUpper(),
            };

            _categoryRepository.Add(entity);

            return new ServiceMessage()
            {
                IsSucceed = true
            };
        }

		public void DeleteCategory(int id)
		{
			_categoryRepository.Delete(id);
		}

		public void EditCategory(CategoryDto categoryDto)
		{
			var entity = _categoryRepository.GetById(categoryDto.Id);
            entity.Name = categoryDto.Name.ToUpper();

            _categoryRepository.Update(entity);
		}

		public CategoryDto GetById(int id)
		{
            var entity = _categoryRepository.GetById(id);
            var categoryDto = new CategoryDto()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
            return categoryDto;
		}

		public List<CategoryDto> GetCategories()
		{
            var entities = _categoryRepository.GetAll().OrderBy(x=>x.Name);

            var categoryDto = entities
                .Include(x=>x.Products)
                .Select(x => new CategoryDto()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
            return categoryDto;
		}
	}
}
