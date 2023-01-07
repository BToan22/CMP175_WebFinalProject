using Microsoft.AspNetCore.Mvc;
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
	}
}