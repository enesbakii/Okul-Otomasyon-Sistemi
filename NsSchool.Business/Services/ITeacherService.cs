using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
    public interface ITeacherService
    {
        ServiceMessage AddTeacher(TeacherAddDto teacherAddDto);
        List<TeacherListDto> GetTeacherList();
        ServiceMessage EditTeacher(TeacherEditDto teacherEditDto);
        TeacherEditDto GetById(int id);
        void DeleteTeacher(int id);
    }
}
