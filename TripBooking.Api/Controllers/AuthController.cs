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
		var refreshExpiry = DateTime.UtcNow.AddDays(7);

		await userService.SetRefreshTokenAsync(user.Id, refreshToken, refreshExpiry, token);

		var response = new Token
		{
			AccessToken = accessToken,
			RefreshToken = refreshToken
		};

		return Ok(response);
	}

	[HttpPost("refresh")]
	public async Task<IActionResult> Refresh([FromBody] Token tokenResponse, CancellationToken token)
	{
		if (string.IsNullOrWhiteSpace(tokenResponse?.RefreshToken))
			return BadRequest(new { message = "Refresh token is required" });

		var user = await userService.GetUserByRefreshTokenAsync(tokenResponse.RefreshToken, token);

		if (user == null || user.RefreshTokenExpiry == null || user.RefreshTokenExpiry < DateTime.UtcNow)
			return Unauthorized(new { message = "Invalid or expired refresh token" });

		var newAccessToken = TokenUtils.GenerateAccessToken(user, JwtSecret);
		var newRefreshToken = TokenUtils.GenerateRefreshToken();
		var newExpiry = DateTime.UtcNow.AddDays(7);

		await userService.SetRefreshTokenAsync(user.Id, newRefreshToken, newExpiry, token);

		var response = new Token
		{
			AccessToken = newAccessToken,
			RefreshToken = newRefreshToken
		};

		return Ok(response);
	}
}
