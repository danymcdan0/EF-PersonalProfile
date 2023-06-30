using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using PersonalProfileAPI.Models.Domains;

namespace PersonalProfileAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<Owner> userManager;
		private readonly ITokenRepository tokenRepository;

		public AuthController(UserManager<Owner> userManager, ITokenRepository tokenRepository)
        {
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
		}

		//POST: /api/Auth/Login
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
		{
			var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);

			if (user != null)
			{
				var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

				if (checkPasswordResult)
				{
					//Get roles for this user
					var roles = await userManager.GetRolesAsync(user);

					if (roles != null)
					{
						//Create Token
						var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

						var response = new LoginResponseDTO
						{
							JwtToken = jwtToken
						};

						return Ok(response);
					}

				}
			}
			return BadRequest("Incorrect username or password");
		}
	}
}
