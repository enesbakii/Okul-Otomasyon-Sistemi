using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
    public interface ICategoryService
    {
        ServiceMessage AddCategory(CategoryDto categoryDto);
        List<CategoryDto> GetCategories();
        CategoryDto GetById(int id);
        void EditCategory(CategoryDto categoryDto);
        void DeleteCategory(int id);
    }
}
