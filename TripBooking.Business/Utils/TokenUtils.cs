using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TripBooking.Data.Entities;

namespace TripBooking.Business.Utils;

public class TokenUtils
{
	public static string GenerateAccessToken(User user, string secret)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(secret);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("id", user.Id.ToString()),
					new Claim("perms", user.Permissions ?? string.Empty)
				}),
			Expires = DateTime.UtcNow.AddMinutes(15),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

	public static string GenerateRefreshToken()
	{
		var randomBytes = new byte[32];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomBytes);
		return Convert.ToBase64String(randomBytes);
	}
}
