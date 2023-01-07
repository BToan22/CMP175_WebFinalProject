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
    }
}