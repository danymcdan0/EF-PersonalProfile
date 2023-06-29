using Microsoft.AspNetCore.Mvc;
using PersonalProfileUI.Models;
using PersonalProfileUI.Models.DTOs;
using System.Drawing.Drawing2D;
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

			try
			{
				var client = httpClientFactory.CreateClient();

				var httpResponseMessage = await client.GetAsync("https://localhost:44385/api/education");

				httpResponseMessage.EnsureSuccessStatusCode();

				response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<EducationDTO>>());

			}
			catch (Exception ex)
			{
				//Log the exception
				throw;
			}
			return View(response);
		}

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            return View();
        }

		[HttpPost]
        public async Task<IActionResult> Add(AddEducationViewModel addEducationViewModel)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:44385/api/education"),
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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<EducationDTO>($"https://localhost:44385/api/education/{Id.ToString()}");

            if (response != null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EducationDTO educationDTO)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:44385/api/education/{educationDTO.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(educationDTO), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EducationDTO>();

            if (response != null)
            {
                return RedirectToAction("Edit", "Education");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EducationDTO educationDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:44385/api/education/{educationDTO.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Education");
            }
            catch (Exception ex)
            {
                //Log or console the error
            }

            return View("Edit");
        }
    }
}
