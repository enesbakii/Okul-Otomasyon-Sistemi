using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NsSchool.Business.Dtos;
using NsSchool.Business.Services;
using NsSchool.WebUI.Areas.Admin.Models;
using System.Diagnostics;

namespace NsSchool.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin,Person")]
	public class ClassRoomController : Controller
	{
		private readonly IClassRoomService _classRoomService;
		private readonly ITeacherService _teacherService;
		private readonly IStudentService _studentService;
		private readonly IClassRoomTeacherService _classTeacherService;
		private readonly IClassRoomStudentService _classStudentService;
		
		public ClassRoomController(IClassRoomService classRoomService, ITeacherService teacherService, IClassRoomTeacherService classTeacherService, IClassRoomStudentService classStudentService, IStudentService studentService)
		{
			_classRoomService = classRoomService;
			_teacherService = teacherService;
			_classTeacherService = classTeacherService;
			_classStudentService = classStudentService;
			_studentService = studentService;

		}
		public IActionResult List()
		{
			var classRoomDtos = _classRoomService.GetClassRoomList();

			var viewModel = classRoomDtos.Select(x=> new ClassRoomListViewModel()
			{
				Id= x.Id,
				Name= x.Name,
			}).ToList();

			return View(viewModel);
		}

		[HttpGet]
		public IActionResult AddClassRoom()
		{

			return View();
		}
		[HttpPost]
		public IActionResult AddClassRoom(ClassRoomAddViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}

			var classRoomDto = new ClassRoomDto()
			{
				Id = formData.Id,
				Name = formData.Name.Trim().ToUpper(),
			};

			var responce = _classRoomService.AddClassRoom(classRoomDto);
			if (responce.IsSucceed)
			{
				return RedirectToAction("list");
			}
			ViewBag.ErrorMessage = responce.Message;
			return View(formData);

		}
		[HttpGet]
		public IActionResult EditClassRoom(int id)
		{
			var classRoomDto = _classRoomService.GetClassRoomById(id);
			var viewModel = new ClassRoomListViewModel()
			{
				Id = id,
				Name = classRoomDto.Name,
			};

			return View(viewModel);
		}
		[HttpPost]
		public IActionResult EditClassRoom(ClassRoomListViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View(formData);
			}

			var classRoomDto = new ClassRoomDto()
			{
				Id = formData.Id,
				Name = formData.Name.Trim().ToUpper(),
			};
			var responce = _classRoomService.UpdateClassRoom(classRoomDto);
			if (!responce.IsSucceed)
			{
				ViewBag.ErrorMessage = responce.Message;
				return View(formData);
				
			}
			return RedirectToAction("list");
		}
		public IActionResult DeleteClassRoom(int id)
		{
			_classRoomService.DeleteClassRoom(id);
			return RedirectToAction("list");
		}


		[HttpGet]
		public IActionResult AddTeacher(int id)
		{

			var classRoomDto = _classRoomService.GetClassRoomById(id);
			var viewModel = new TeacherClassRoomViewModel()
			{
				Id = id,
				ClassRoom = classRoomDto.Name,
			};
			var dto = _teacherService.GetTeacherList();
			var teachers = dto.Select(x => new TeacherClassRoomViewModel()
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName,
			}).ToList();
			var teacherList = new List<SelectListItem>();
			foreach (var teacher in teachers)
			{
				teacherList.Add(new SelectListItem
				{
					Text = teacher.FullName,
					Value = teacher.Id.ToString(),
				});
			}

			ViewBag.TeacherList = teacherList;
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult AddTeacher(string[] ClassTeacher , TeacherClassRoomViewModel formData)
		{
			var dto = _teacherService.GetTeacherList();
			var teachers = dto.Select(x => new TeacherClassRoomViewModel()
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName,
			}).ToList();
			var teacherList = new List<SelectListItem>();
			foreach (var teacher in teachers)
			{
				teacherList.Add(new SelectListItem
				{
					Text = teacher.FullName,
					Value = teacher.Id.ToString(),
				});
			}


			var classroomTeacherDtoList = new List<ClassRoomTeacherDto>();
			
			for (int i = 0; i < ClassTeacher.Length; i++)
			{
				var classRoomTeacherDto = new ClassRoomTeacherDto()
				{

					TeacherId = Convert.ToInt32(ClassTeacher[i]),
					ClassRoomId = formData.Id,
					Name = formData.ClassRoom									
				};

				classroomTeacherDtoList.Add(classRoomTeacherDto);
			}
			


			var responce =_classTeacherService.AddClassRoomTeacher(classroomTeacherDtoList);
			if (!responce.IsSucceed)
			{
					ViewBag.TeacherList = teacherList;
				ViewBag.ErrorMessage = responce.Message;
				return View(formData);
				
			}

			return RedirectToAction("list");
		}
		public IActionResult Index(int id)
		{
			ViewBag.ClassRoomId = id;
			var teacherDtos = _classTeacherService.ClassRoomTeacherList(id);
			var classRoomTeacherList = teacherDtos.Select(x => new ClassRoomTeacherListViewModel()
			{
				ClassRoomId = x.ClassRoomId,
				LastName= x.LastName,
				FirstName= x.FirstName,
				Branch= x.Branch,
				TeacherId=x.TeacherId,
				
			}).ToList();

			var studentDtos = _classStudentService.ClassRoomStudentList(id);
			var classRoomStudentList = studentDtos.Select(x=> new StudentClassRoomViewModel()
			{
				StudentId = x.StudentId,
				ClassRoomId=x.ClassRoomId,
				FirstName= x.FirstName,
				LastName= x.LastName,
				ClassRoomName=x.ClassRoom
			}).ToList();




			var viewModel = new ClassRoomStudentTeacherListViewModel();
			viewModel.TeacherClassRoomList = classRoomTeacherList;
			viewModel.StudentClassRoomList = classRoomStudentList;





			return View(viewModel);
			
		}
		public IActionResult DeleteTeacher(int teacherId, int classRoomId)
		{
			var dto = new ClassRoomTeacherDto()
			{
				ClassRoomId = classRoomId,
				TeacherId = teacherId
			};
			_classTeacherService.DeleteTeacher(dto);
			return RedirectToAction("List");
		}
		[HttpGet]
		public IActionResult AddStudent(int id)
		{
			var classRoomDto = _classRoomService.GetClassRoomById(id);
			var viewModel = new StudentClassRoomViewModel()
			{
				ClassRoomId = id,
				ClassRoomName = classRoomDto.Name,
			};
			var classRoomDtoNewClassName = "";
			int pos = viewModel.ClassRoomName.IndexOf("-");
			classRoomDtoNewClassName = viewModel.ClassRoomName.Substring(0, pos);
			var dto = _studentService.GetAll();
			var students = dto.Where(x=>x.Grade.StartsWith(classRoomDtoNewClassName)).Select(x=> new StudentClassRoomViewModel()
			{
				ClassRoomId= x.Id,
				StudentId=x.Id,
				FirstName=x.StdentFirstName,
				LastName=x.StudentLastName,
				
			}).ToList();
			
			var studentList = new List<SelectListItem>();
			foreach (var student in students)
			{
				studentList.Add(new SelectListItem
				{
					Text = student.FullName,
					Value = student.StudentId.ToString(),
				});
			}

			ViewBag.StudentList = studentList;

			return View(viewModel);
		}
		public IActionResult AddStudent(string[] classStudent, StudentClassRoomViewModel formData)
		{
			var classRoomDtoNewClassName = "";
			int pos = formData.ClassRoomName.IndexOf("-");
			classRoomDtoNewClassName = formData.ClassRoomName.Substring(0, pos);


			var dto = _studentService.GetAll();
			var students = dto.Where(x=>x.Grade.StartsWith(classRoomDtoNewClassName)).Select(x => new StudentClassRoomViewModel()
			{
				ClassRoomId = x.Id,
				StudentId = x.Id,
				FirstName = x.StdentFirstName,
				LastName = x.StudentLastName
			}).ToList();

			var studentList = new List<SelectListItem>();
			foreach (var student in students)
			{
				studentList.Add(new SelectListItem
				{
					Text = student.FullName,
					
					Value = student.StudentId.ToString(),
					
				});
			}

			var classRoomStudentDtos = new List<ClassRoomStudentDto>();

			for (int i = 0; i < classStudent.Length; i++)
			{
				var classRoomStudentDto = new ClassRoomStudentDto()
				{

					StudentId = Convert.ToInt32(classStudent[i]),
					ClassRoomId = formData.ClassRoomId,
				

				};

				classRoomStudentDtos.Add(classRoomStudentDto);
			}



			var responce = _classStudentService.AddStudent(classRoomStudentDtos);
			if (!responce.IsSucceed)
			{
				ViewBag.StudentList = studentList;
				ViewBag.ErrorMessage = responce.Message;
				return View(formData);

			}
			return RedirectToAction("list");
		}	

		public IActionResult DeleteStudent(int studentId,int classRoomId)
		{
			var dto = new ClassRoomStudentDto()
			{
				ClassRoomId = classRoomId,
				StudentId = studentId
			};
			_classStudentService.DeleteStudent(dto);
			return RedirectToAction("list");
		}
		
	}
	
}
