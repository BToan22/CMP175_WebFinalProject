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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                return Redirect("login");
            else
            {
                var model = new
                {
                    username = HttpContext.Session.GetString("UserName"),
                    fullname = HttpContext.Session.GetString("FullName")
                };
                return View(model);
            }
        }

		public IActionResult Privacy()
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