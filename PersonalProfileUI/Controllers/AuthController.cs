using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using PersonalProfileUI.Models;
using PersonalProfileUI.Models.DTOs;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PersonalProfileUI.Controllers
{
	public class AuthController : Controller
	{
        private readonly IHttpClientFactory httpClientFactory;
        public string Token { get; set; }

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
            try
            {
                var client = httpClientFactory.CreateClient();

				var httpRequestMessage = new HttpRequestMessage()
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("https://localhost:44385/api/Auth/Login"),
					Content = new StringContent(JsonSerializer.Serialize(loginRequestDTO), Encoding.UTF8, "application/json")
				};

				var httpResponseMessage = await client.SendAsync(httpRequestMessage);
				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<LoginResponseDTO>();

                Token = response.JwtToken;

				HttpContext.Response.Cookies.Append("token", Token,
				new CookieOptions { Expires = DateTime.Now.AddMinutes(15) });
			}

            catch (Exception)
            {
				return RedirectToAction("Index", "Auth");
			}
            return RedirectToAction("Index", "Home");
		}

        public async Task<string> GetToken() 
        {
			var client = httpClientFactory.CreateClient();
            return "";
		}
	}
}