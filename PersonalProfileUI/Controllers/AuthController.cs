using Microsoft.AspNetCore.Mvc;

namespace PersonalProfileUI.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Submit() 
		{
			return View();
		}
	}
}
