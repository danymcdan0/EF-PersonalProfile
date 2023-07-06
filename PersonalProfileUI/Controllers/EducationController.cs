using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalProfileUI.Models;
using PersonalProfileUI.Models.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PersonalProfileUI.Controllers
{
	public class EducationController : Controller
	{
		private readonly IHttpClientFactory httpClientFactory;

		public EducationController(IHttpClientFactory httpClientFactory)
        {
			this.httpClientFactory = httpClientFactory;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<EducationDTO> response = new List<EducationDTO>();

			var client = httpClientFactory.CreateClient();

			var httpResponseMessage = await client.GetAsync("https://app-personalprofile-dev.azurewebsites.net/api/education");

			httpResponseMessage.EnsureSuccessStatusCode();

			response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<EducationDTO>>());

			return View(response);
		}

        [HttpGet]
		//[Authorize(Roles ="Owner")]
        public async Task<IActionResult> Add() 
        {
			var client = httpClientFactory.CreateClient();

			string token = HttpContext.User.FindFirst("TokenClaim").Value;

			if (token != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Auth");
        }

		[HttpPost]
        public async Task<IActionResult> Add(AddEducationViewModel addEducationViewModel)
        {
            try
            {
				var client = httpClientFactory.CreateClient();

				string token = HttpContext.User.FindFirst("TokenClaim").Value;

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var httpRequestMessage = new HttpRequestMessage()
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("https://app-personalprofile-dev.azurewebsites.net/api/education"),
					Content = new StringContent(JsonSerializer.Serialize(addEducationViewModel), Encoding.UTF8, "application/json")
				};

				var httpResponseMessage = await client.SendAsync(httpRequestMessage);
				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<EducationDTO>();

				if (response != null)
				{
					return RedirectToAction("Index", "Education");
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

			string token = HttpContext.User.FindFirst("TokenClaim").Value;

			if (token == null)
			{
				return RedirectToAction("Index", "Auth");
			}

			var response = await client.GetFromJsonAsync<EducationDTO>($"https://app-personalprofile-dev.azurewebsites.net/api/education/{Id.ToString()}");

            if (response != null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EducationDTO educationDTO)
        {
            try
            {
				var client = httpClientFactory.CreateClient();
				string token = HttpContext.User.FindFirst("TokenClaim").Value;

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var httpRequestMessage = new HttpRequestMessage()
				{
					Method = HttpMethod.Put,
					RequestUri = new Uri($"https://app-personalprofile-dev.azurewebsites.net/api/education/{educationDTO.Id}"),
					Content = new StringContent(JsonSerializer.Serialize(educationDTO), Encoding.UTF8, "application/json")
				};

				var httpResponseMessage = await client.SendAsync(httpRequestMessage);
				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<EducationDTO>();

				if (response == null)
				{
					return RedirectToAction("Edit", "Education");
				}

				return RedirectToAction("Index", "Education"); ;
			}
            catch (Exception)
            {
				return RedirectToAction("Index", "Auth");
			}

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EducationDTO educationDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

				string token = HttpContext.User.FindFirst("TokenClaim").Value;

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var httpResponseMessage = await client.DeleteAsync($"https://app-personalprofile-dev.azurewebsites.net/api/education/{educationDTO.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Education");
            }
            catch (Exception)
            {
				return RedirectToAction("Index", "Auth");
			}
        }
    }
}
