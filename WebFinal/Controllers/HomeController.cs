using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebFinal.Models;

namespace WebFinal.Controllers
{

	public class HomeController : Controller
	{

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
				return Redirect("Login");
			else
			{
				var model = new
				{
					username = HttpContext.Session.GetString("Username"),
					fullname = HttpContext.Session.GetString("Fullname")
				};
				return View(model);
			}

		}

		public IActionResult Student()
		{

			return View();

		}
		public IActionResult Lecturer()
		{
			return View();
		}
		public IActionResult Subjects()
		{
			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		[HttpPost]
		public IActionResult get_course(string Major, int Page, int Size)
		{
			var data = getCourse(Major, Page, Size);
			if (data != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = data
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}
		}

		public IActionResult delete_course(int id)
		{
			var c = deleteCourse(id);
			if (c != null)
			{
				var res = new
				{
					Success = c,
					Message = "",

				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}
		}

		public IActionResult update_course(Subject subject)
		{
			var c = updateCourse(subject);
			if (c != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = c
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}

		}
		public IActionResult insert_course(Subject subject)
		{
			var c = insertCourse(subject);
			if (c != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = c
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}

		}
		[HttpPost]
		public IActionResult get_student(int Page, int Size)
		{
			var data = getStudent(Page, Size);
			if (data != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = data
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}
		}

		public IActionResult delete_student(int id)
		{
			var c = deleteStudent(id);
			if (c != null)
			{
				var res = new
				{
					Success = c,
					Message = "",

				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}
		}

		public IActionResult insert_student(Student student)
		{
			var c = insertStudent(student);
			if (c != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = c
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}

		}
		public IActionResult update_student(Student student)
		{
			var c = updateStudent(student);
			if (c != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = c
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}

		}
		[HttpPost]
		public IActionResult get_lecturer(int Page, int Size)
		{
			var data = getLecturer(Page, Size);
			if (data != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = data
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}
		}

		public IActionResult delete_lecturer(int id)
		{
			var c = deleteLecturer(id);
			if (c != null)
			{
				var res = new
				{
					Success = c,
					Message = "",

				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}
		}

		public IActionResult insert_lecturer(Lecturer lecturer)
		{
			var c = insertLecturer(lecturer);
			if (c != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = c
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}

		}
		public IActionResult update_lecturer(Lecturer lecturer)
		{
			var c = updateLecturer(lecturer);
			if (c != null)
			{
				var res = new
				{
					Success = true,
					Message = "",
					Data = c
				};
				return Json(res);
			}
			else
			{
				var res = new
				{
					Success = false,
					Message = "ERROR",
				};
				return Json(res);
			}

		}

		//PRIVATE
		private object? getCourse(string mj, int page, int size)
		{
			try
			{
				var db = new WebFinalContext();
				var ls = db.Subjects.Where(x => x.Major == mj);

				var offset = (page - 1) * size;
				var totalRecord = ls.Count();
				var totalPage = (totalRecord % size) == 0 ?
					(int)(totalRecord / size) :
					(int)(totalRecord / size + 1);
				var lst = ls.Skip(offset).Take(size).ToList();
				return new
				{
					Data = lst,
					TotalRecord = totalRecord,
					TotalPage = totalPage,
					Page = page,
					Size = size
				};
			}
			catch (Exception ex)
			{
				return null;
			}

		}

		private Boolean? deleteCourse(int? id)
		{
			try
			{
				if (id == null)
					return null;
				var db = new WebFinalContext();
				var c1 = db.Subjects.Where(x => x.Id == id).FirstOrDefault();
				if (c1 != null)
				{
					db.Subjects.Remove(c1);
					db.SaveChanges();
					return true;
				}
				else
					return false;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private object? insertCourse(Subject c)
		{
			try
			{
				if (c == null)
					return null;
				var db = new WebFinalContext();
				var c1 = new Subject();

				if (c1.Major != c.Major)
					c1.Major = c.Major;
				if (c1.Credits != c.Credits)
					c1.Credits = c.Credits;
				if (c1.SubjectName != c.SubjectName)
					c1.SubjectName = c.SubjectName;
				if (c1.SubjectId != c.SubjectId)
					c1.SubjectId = c.SubjectId;
				db.Subjects.Add(c1);
				db.SaveChanges();
				return c1;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private object? updateCourse(Subject c)
		{
			try
			{
				if (c == null)
					return null;
				var db = new WebFinalContext();
				var c1 = db.Subjects.Where(x => x.Id == c.Id).FirstOrDefault();
				if (c1.Major != c.Major)
					c1.Major = c.Major;
				if (c1.Credits != c.Credits)
					c1.Credits = c.Credits;

				if (c1.SubjectName != c.SubjectName)
					c1.SubjectName = c.SubjectName;
				if (c1.SubjectId != c.SubjectId)
					c1.SubjectId = c.SubjectId;
				db.Subjects.Update(c1);
				db.SaveChanges();
				return c1;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		//STUDENT


		private object? getStudent(int page, int size)
		{
			try
			{
				var db = new WebFinalContext();
				var ls = db.Students.ToList();

				var offset = (page - 1) * size;
				var totalRecord = ls.Count();
				var totalPage = (totalRecord % size) == 0 ?
					(int)(totalRecord / size) :
					(int)(totalRecord / size + 1);
				var lst = ls.Skip(offset).Take(size).ToList();
				return new
				{
					Data = lst,
					TotalRecord = totalRecord,
					TotalPage = totalPage,
					Page = page,
					Size = size
				};
			}
			catch (Exception ex)
			{
				return null;
			}

		}
		private Boolean? deleteStudent(int? id)
		{
			try
			{
				if (id == null)
					return null;
				var db = new WebFinalContext();
				var c1 = db.Students.Where(x => x.Id == id).FirstOrDefault();
				if (c1 != null)
				{
					db.Students.Remove(c1);
					db.SaveChanges();
					return true;
				}
				else
					return false;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private object? insertStudent(Student s)
		{
			try
			{
				if (s == null)
					return null;
				var db = new WebFinalContext();
				var s1 = new Student();

				if (s1.StudentEmail != s.StudentEmail)
					s1.StudentEmail = s.StudentEmail;
				if (s1.StudentGender != s.StudentGender)
					s1.StudentGender = s.StudentGender;
				if (s1.StudentTown != s.StudentTown)
					s1.StudentTown = s.StudentTown;
				if (s1.StudentPhone != s.StudentPhone)
					s1.StudentPhone = s.StudentPhone;
				if (s1.StudentName != s.StudentName)
					s1.StudentName = s.StudentName;
				if (s1.StudentId != s.StudentId)
					s1.StudentId = s.StudentId;
				db.Students.Add(s1);
				db.SaveChanges();
				return s1;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private object? updateStudent(Student s)
		{
			try
			{
				if (s == null)
					return null;
				var db = new WebFinalContext();
				var s1 = db.Students.Where(x => x.Id == s.Id).FirstOrDefault();

				if (s1.StudentEmail != s.StudentEmail)
					s1.StudentEmail = s.StudentEmail;
				if (s1.StudentGender != s.StudentGender)
					s1.StudentGender = s.StudentGender;
				if (s1.StudentTown != s.StudentTown)
					s1.StudentTown = s.StudentTown;
				if (s1.StudentPhone != s.StudentPhone)
					s1.StudentPhone = s.StudentPhone;
				if (s1.StudentName != s.StudentName)
					s1.StudentName = s.StudentName;
				if (s1.StudentId != s.StudentId)
					s1.StudentId = s.StudentId;

				db.Students.Update(s1);
				db.SaveChanges();
				return s1;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		//LECTURER
		private object? getLecturer(int page, int size)
		{
			try
			{
				var db = new WebFinalContext();
				var ls = db.Lecturers.ToList();

				var offset = (page - 1) * size;
				var totalRecord = ls.Count();
				var totalPage = (totalRecord % size) == 0 ?
					(int)(totalRecord / size) :
					(int)(totalRecord / size + 1);
				var lst = ls.Skip(offset).Take(size).ToList();
				return new
				{
					Data = lst,
					TotalRecord = totalRecord,
					TotalPage = totalPage,
					Page = page,
					Size = size
				};
			}
			catch (Exception ex)
			{
				return null;
			}

		}
		private Boolean? deleteLecturer(int? id)
		{
			try
			{
				if (id == null)
					return null;
				var db = new WebFinalContext();
				var l1 = db.Lecturers.Where(x => x.Id == id).FirstOrDefault();
				if (l1 != null)
				{
					db.Lecturers.Remove(l1);
					db.SaveChanges();
					return true;
				}
				else
					return false;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private object? insertLecturer(Lecturer l)
		{
			try
			{
				if (l == null)
					return null;
				var db = new WebFinalContext();
				var l1 = new Lecturer();

				if (l1.LecturerEmail != l.LecturerEmail)
					l1.LecturerEmail = l.LecturerEmail;

				if (l1.LecturerPhone != l.LecturerPhone)
					l1.LecturerPhone = l.LecturerPhone;
				if (l1.LecturerName != l.LecturerName)
					l1.LecturerName = l.LecturerName;
				if (l1.LecturerId != l.LecturerId)
					l1.LecturerId = l.LecturerId;
				db.Lecturers.Add(l1);
				db.SaveChanges();
				return l1;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private object? updateLecturer(Lecturer l)
		{
			try
			{
				if (l == null)
					return null;
				var db = new WebFinalContext();
				var l1 = db.Lecturers.Where(x => x.Id == l.Id).FirstOrDefault();

				if (l1.LecturerEmail != l.LecturerEmail)
					l1.LecturerEmail = l.LecturerEmail;
				if (l1.LecturerPhone != l.LecturerPhone)
					l1.LecturerPhone = l.LecturerPhone;
				if (l1.LecturerName != l.LecturerName)
					l1.LecturerName = l.LecturerName;
				if (l1.LecturerId != l.LecturerId)
					l1.LecturerId = l.LecturerId;

				db.Lecturers.Update(l1);
				db.SaveChanges();
				return l1;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}