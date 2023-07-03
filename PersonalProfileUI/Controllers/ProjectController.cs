using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using PersonalProfileUI.Models;
using PersonalProfileUI.Models.DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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

			var client = httpClientFactory.CreateClient();

			var httpResponseMessage = await client.GetAsync("https://app-personalprofile-dev.azurewebsites.net/api/Project");

			httpResponseMessage.EnsureSuccessStatusCode();

			response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProjectDTO>>());

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
		public async Task<IActionResult> Add(AddProjectViewModel addProjectViewModel)
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
					RequestUri = new Uri("https://app-personalprofile-dev.azurewebsites.net/api/Project"),
					Content = new StringContent(JsonSerializer.Serialize(addProjectViewModel), Encoding.UTF8, "application/json")
				};

				var httpResponseMessage = await client.SendAsync(httpRequestMessage);
				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<ProjectDTO>();

				if (response != null)
				{
					return RedirectToAction("Index", "Project");
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

			var response = await client.GetFromJsonAsync<ProjectDTO>($"https://app-personalprofile-dev.azurewebsites.net/api/Project/{Id.ToString()}");

			if (response != null)
			{
				return View(response);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ProjectDTO projectDTO)
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
					RequestUri = new Uri($"https://app-personalprofile-dev.azurewebsites.net/api/Project/{projectDTO.Id}"),
					Content = new StringContent(JsonSerializer.Serialize(projectDTO), Encoding.UTF8, "application/json")
				};

				var httpResponseMessage = await client.SendAsync(httpRequestMessage);
				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<ProjectDTO>();

				if (response == null)
				{
					return RedirectToAction("Edit", "Project");
				}

				return RedirectToAction("Index", "Project"); ;
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Auth");
			}

		}

		[HttpPost]
		public async Task<IActionResult> Delete(ProjectDTO projectDTO)
		{
			try
			{
				var client = httpClientFactory.CreateClient();

				var token = "";

				HttpContext.Request.Cookies.TryGetValue("token", out token);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var httpResponseMessage = await client.DeleteAsync($"https://app-personalprofile-dev.azurewebsites.net/api/Project/{projectDTO.Id}");

				httpResponseMessage.EnsureSuccessStatusCode();

				return RedirectToAction("Index", "Project");
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Auth");
			}
		}
	}
}
