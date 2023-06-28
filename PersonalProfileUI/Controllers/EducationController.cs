using Microsoft.AspNetCore.Mvc;
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
				//Get all regions from Web API
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

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7096/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EducationDTO>();

            if (response != null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<EducationDTO>($"https://localhost:7096/api/education/{Id.ToString()}");

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
                RequestUri = new Uri($"https://localhost:7096/api/regions/{educationDTO.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(educationDTO), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EducationDTO>();

            if (response != null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EducationDTO regionDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7096/api/regions/{regionDTO.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                //Log or console the error
            }

            return View("Edit");
        }
    }
}
