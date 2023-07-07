using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PersonalProfileUI.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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
		public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
		{
            var client = httpClientFactory.CreateClient();

			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("https://app-personalprofile-dev.azurewebsites.net/api/Auth/Login"),
				Content = new StringContent(JsonSerializer.Serialize(loginRequestDTO), Encoding.UTF8, "application/json")
			};

			var httpResponseMessage = await client.SendAsync(httpRequestMessage);
			if (!httpResponseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Auth");
			}

			else
			{
				var token = await GetJWTTokenStringFromHttpResponse(httpResponseMessage);

				await SignInUser(GetUserRoleFromJWTTokenString(token), token);

				return RedirectToAction("Index", "Home");
			}
		}

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("PP_auth_cookie");
            return RedirectToAction("Index", "Home");
        }

        public async Task SignInUser(string userRole, string userToken)
		{
			var claims = new List<Claim>
			{
				new Claim("TokenClaim", userToken),
				new Claim(ClaimTypes.Role, userRole)
			};

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity));
		}

		public string GetUserRoleFromJWTTokenString(string tokenString)
		{
			var handler = new JwtSecurityTokenHandler();
			var jsonToken = handler.ReadToken(tokenString);
			var tokenS = jsonToken as JwtSecurityToken;
			//
			// Possible need for exception.
			// 
			if (tokenS is null) return "";
			var claim = tokenS.Claims.FirstOrDefault(j => j.Type.EndsWith("/role"));
			return claim is null ? "" : claim.Value;
		}

		public async Task<string> GetJWTTokenStringFromHttpResponse(HttpResponseMessage httpResponseMessage)
		{
			if (httpResponseMessage.Content is null) throw new ArgumentNullException("No content response given");

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<LoginResponseDTO>();
			if (response is not null)
			{
				return response.JwtToken;
			}
			else
			{
				throw new ArgumentNullException("Response object is null");
			}
		}
	}
}