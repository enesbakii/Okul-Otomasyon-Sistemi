using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Student.Models;
using NsSchool.WebUI.Extensions;

namespace NsSchool.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles ="Student")]
    public class TeacherController : Controller
    {
        private readonly IClassRoomTeacherService _classRoomTeacherService;
        public TeacherController(IClassRoomTeacherService classRoomTeacherService)
        {
            _classRoomTeacherService = classRoomTeacherService;
        }

        public IActionResult List()
        {
           var dto = _classRoomTeacherService.ListTeacher(User.GetId());
            var viewModel = dto.Select(x=> new ClassRoomTeacherViewModel()
            {
                Branch= x.Branch,
                FirstName= x.FirstName,
                LastName= x.LastName,
            }).ToList();
            return View(viewModel);
        }
    }
}
