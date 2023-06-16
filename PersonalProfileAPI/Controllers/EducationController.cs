using Microsoft.AspNetCore.Mvc;

namespace PersonalProfileAPI.Controllers
{
	public class EducationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
