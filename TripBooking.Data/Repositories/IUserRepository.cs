using TripBooking.Data.Entities;

namespace TripBooking.Data.Repositories;

public interface IUserRepository
{
	Task<User?> GetUserByUsernameAsync(string username, CancellationToken token);
	Task SetRefreshTokenAsync(int userId, string refreshToken, DateTime expiry, CancellationToken token);
	Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken token);
}
