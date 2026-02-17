using TripBooking.Data.Entities;
using TripBooking.Data.Repositories;

namespace TripBooking.Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
	public async Task<User?> Authenticate(string username, string password, CancellationToken token)
	{
		var user = await userRepository.GetUserByUsernameAsync(username, token);

		// In a real application, you should hash the password and compare the hash instead of storing plain text passwords.
		if (user == null || user.Password != password)
			return null;

		return user;
	}
}
