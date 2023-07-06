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

			var httpResponseMessage = await client.GetAsync("https://app-personalprofile-dev.azurewebsites.net/api/Experience");

			httpResponseMessage.EnsureSuccessStatusCode();

			response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ExperienceDTO>>());

			return View(response);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			string token = HttpContext.User.FindFirst("TokenClaim").Value;
			if (token != null)
			{
				return View();
			}

			return RedirectToAction("Index", "Auth");
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddExperienceViewModel addExperienceViewModel)
		{
			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("https://app-personalprofile-dev.azurewebsites.net/api/Experience"),
				Content = new StringContent(JsonSerializer.Serialize(addExperienceViewModel), Encoding.UTF8, "application/json")
			};

			var httpResponseMessage = await ClientResponse(httpRequestMessage);
			if (!httpResponseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Auth");
			}

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<ExperienceDTO>();
			if (response != null)
			{
				return RedirectToAction("Index", "Experience");
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid Id)
		{
			string token = HttpContext.User.FindFirst("TokenClaim").Value;
			if (token == null)
			{
				return RedirectToAction("Index", "Auth");
			}

			var client = httpClientFactory.CreateClient();
			var response = await client.GetFromJsonAsync<ExperienceDTO>($"https://app-personalprofile-dev.azurewebsites.net/api/Experience/{Id.ToString()}");

			if (response != null)
			{
				return View(response);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ExperienceDTO experienceDTO)
		{
			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri($"https://app-personalprofile-dev.azurewebsites.net/api/Experience/{experienceDTO.Id}"),
				Content = new StringContent(JsonSerializer.Serialize(experienceDTO), Encoding.UTF8, "application/json")
			};

			var httpResponseMessage = await ClientResponse(httpRequestMessage);
			if (!httpResponseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Auth");
			}

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<ExperienceDTO>();
			if (response == null)
			{
				return RedirectToAction("Edit", "Experience");
			}

			return RedirectToAction("Index", "Experience"); ;
		}

		[HttpPost]
		public async Task<IActionResult> Delete(ExperienceDTO experienceDTO)
		{
			var client = httpClientFactory.CreateClient();
			string token = HttpContext.User.FindFirst("TokenClaim").Value;

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var httpResponseMessage = await client.DeleteAsync($"https://app-personalprofile-dev.azurewebsites.net/api/Experience/{experienceDTO.Id}");

			if (!httpResponseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Auth");
			}
			return RedirectToAction("Index", "Experience");
		}

		private async Task<HttpResponseMessage> ClientResponse(HttpRequestMessage message)
		{
			var client = httpClientFactory.CreateClient();

			string token = HttpContext.User.FindFirst("TokenClaim").Value;

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			return await client.SendAsync(message);
		}
	}
}

