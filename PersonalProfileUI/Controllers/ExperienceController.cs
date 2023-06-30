using Microsoft.AspNetCore.Mvc;
using PersonalProfileUI.Models;
using PersonalProfileUI.Models.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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

			var client = httpClientFactory.CreateClient();

			var httpResponseMessage = await client.GetAsync("https://localhost:44385/api/Experience");

			httpResponseMessage.EnsureSuccessStatusCode();

			response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ExperienceDTO>>());

			return View(response);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var client = httpClientFactory.CreateClient();

			var token = "";
			HttpContext.Request.Cookies.TryGetValue("token", out token);


			if (token != null)
			{
				return View();
			}

			return RedirectToAction("Index", "Auth");
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddExperienceViewModel addExperienceViewModel)
		{
			try
			{
				var client = httpClientFactory.CreateClient();

				var token = "";

				HttpContext.Request.Cookies.TryGetValue("token", out token);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var httpRequestMessage = new HttpRequestMessage()
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("https://localhost:44385/api/Experience"),
					Content = new StringContent(JsonSerializer.Serialize(addExperienceViewModel), Encoding.UTF8, "application/json")
				};

				var httpResponseMessage = await client.SendAsync(httpRequestMessage);
				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<ExperienceDTO>();

				if (response != null)
				{
					return RedirectToAction("Index", "Experience");
				}

				return View();
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Auth");
			}

		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid Id)
		{
			var client = httpClientFactory.CreateClient();

			var token = "";
			HttpContext.Request.Cookies.TryGetValue("token", out token);

			if (token == null)
			{
				return RedirectToAction("Index", "Auth");
			}

			var response = await client.GetFromJsonAsync<ExperienceDTO>($"https://localhost:44385/api/Experience/{Id.ToString()}");

			if (response != null)
			{
				return View(response);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ExperienceDTO experienceDTO)
		{
			try
			{
				var client = httpClientFactory.CreateClient();
				var token = "";

				HttpContext.Request.Cookies.TryGetValue("token", out token);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var httpRequestMessage = new HttpRequestMessage()
				{
					Method = HttpMethod.Put,
					RequestUri = new Uri($"https://localhost:44385/api/Experience/{experienceDTO.Id}"),
					Content = new StringContent(JsonSerializer.Serialize(experienceDTO), Encoding.UTF8, "application/json")
				};

				var httpResponseMessage = await client.SendAsync(httpRequestMessage);
				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<ExperienceDTO>();

				if (response == null)
				{
					return RedirectToAction("Edit", "Experience");
				}

				return RedirectToAction("Index", "Experience"); ;
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Auth");
			}

		}

		[HttpPost]
		public async Task<IActionResult> Delete(ExperienceDTO experienceDTO)
		{
			try
			{
				var client = httpClientFactory.CreateClient();

				var token = "";

				HttpContext.Request.Cookies.TryGetValue("token", out token);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var httpResponseMessage = await client.DeleteAsync($"https://localhost:44385/api/Experience/{experienceDTO.Id}");

				httpResponseMessage.EnsureSuccessStatusCode();

				return RedirectToAction("Index", "Experience");
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Auth");
			}
		}
	}
}

