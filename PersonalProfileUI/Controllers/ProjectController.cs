using Microsoft.AspNetCore.Mvc;
using PersonalProfileUI.Models.DTOs;
using System.Net.Http;

namespace PersonalProfileUI.Controllers
{
	public class ProjectController : Controller
	{
		private readonly IHttpClientFactory httpClientFactory;

		public ProjectController(IHttpClientFactory httpClientFactory)
        {
			this.httpClientFactory = httpClientFactory;
		}

        [HttpGet]
		public async Task<IActionResult> Index()
		{
			List<ProjectDTO> response = new List<ProjectDTO>();

			try
			{
				//Get all regions from Web API
				var client = httpClientFactory.CreateClient();

				var httpResponseMessage = await client.GetAsync("https://localhost:44385/api/project");

				httpResponseMessage.EnsureSuccessStatusCode();

				response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProjectDTO>>());

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
