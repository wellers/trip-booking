using TripBooking.Data.Entities;
using TripBooking.Data.Repositories;

namespace TripBooking.Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
	public async Task<User?> Authenticate(string username, string password, CancellationToken token)
	{
		var user = await userRepository.GetUserByUsernameAsync(username, token);

		if (user == null)
			return null;

		var ok = Utils.CryptoUtils.VerifyPassword(password, user.Password);
		if (!ok) 
			return null;

		return user;
	}

	public async Task SetRefreshTokenAsync(int userId, string refreshToken, DateTime expiry, CancellationToken token)
	{
		var hashed = Utils.CryptoUtils.HashRefreshToken(refreshToken);
		await userRepository.SetRefreshTokenAsync(userId, hashed, expiry, token);
	}

	public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken token)
	{
		var hashed = Utils.CryptoUtils.HashRefreshToken(refreshToken);
		return await userRepository.GetUserByRefreshTokenAsync(hashed, token);
	}
}
