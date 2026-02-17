using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TripBooking.Business.Services;
using TripBooking.Business.Utils;
using TripBooking.Shared.Request;
using TripBooking.Shared.Response;

namespace TripBooking.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
public class AuthController(ILogger<AuthController> logger, IConfiguration configuration, IUserService userService) : ControllerBase
{	
	private readonly string JwtSecret = configuration["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret");

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] Login loginRequest, CancellationToken token)
	{
		var user = await userService.Authenticate(loginRequest.Username, loginRequest.Password, token);

		if (user == null)
		{
			logger.LogWarning("Authentication failed for user {Username}", loginRequest.Username);
			return Unauthorized(new { message = "Invalid username or password" });
		}

		var accessToken = TokenUtils.GenerateAccessToken(user, JwtSecret);
		var refreshToken = TokenUtils.GenerateRefreshToken();

		var response = new Token
		{
			AccessToken = accessToken,
			RefreshToken = refreshToken
		};

		return Ok(response);
	}

	[HttpPost("refresh")]
	public IActionResult Refresh([FromBody] Token tokenResponse)
	{
		var newAccessToken = TokenUtils.GenerateAccessTokenFromRefreshToken(tokenResponse.RefreshToken, JwtSecret);

		var response = new Token
		{
			AccessToken = newAccessToken,
			RefreshToken = tokenResponse.RefreshToken
		};

		return Ok(response);
	}
}
