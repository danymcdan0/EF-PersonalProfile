using Microsoft.AspNetCore.Mvc;
using PersonalProfileUI.Models.DTOs;
using System.Net.Http;

namespace PersonalProfileUI.Controllers
{
	public class AuthController : Controller
	{
        private readonly IHttpClientFactory httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Submit(LoginRequestDTO loginRequestDTO)
		{
            //TODO need to be able to call this method from login button
            //TODO need to send the data from the DTO to the API
            try
            {
                //Get all regions from Web API
                var client = httpClientFactory.CreateClient();


                //something here -> client(loginRequestDTO)

                var httpResponseMessage = await client.GetAsync("https://localhost:44385/api/Auth/Login");

                httpResponseMessage.EnsureSuccessStatusCode();

                //? This should be sending this data to the http client not reading from it
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<LoginRequestDTO>();

            }
            catch (Exception ex)
            {
                //Log the exception
                throw;
            }
            return View();
		}
	}
}
