using Microsoft.AspNetCore.Mvc;
using PersonalProfileUI.Models.DTOs;

namespace PersonalProfileUI.Controllers
{
	public class ExperienceController : Controller
	{
		private readonly IHttpClientFactory httpClientFactory;

		public ExperienceController(IHttpClientFactory httpClientFactory)
		{
			this.httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<ExperienceDTO> response = new List<ExperienceDTO>();

			try
			{
				//Get all regions from Web API
				var client = httpClientFactory.CreateClient();

				var httpResponseMessage = await client.GetAsync("https://localhost:44385/api/experience");

				httpResponseMessage.EnsureSuccessStatusCode();

				response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ExperienceDTO>>());

			}
			catch (Exception ex)
			{
				//Log the exception
				throw;
			}
			return View(response);
		}
	}
}

