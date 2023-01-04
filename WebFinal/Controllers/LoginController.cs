using Microsoft.AspNetCore.Mvc;

namespace WebFinal.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
