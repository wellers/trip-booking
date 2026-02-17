using TripBooking.Data.Entities;

namespace TripBooking.Business.Services;

public interface IUserService
{
	Task<User?> Authenticate(string username, string password, CancellationToken token);
	Task SetRefreshTokenAsync(int userId, string refreshToken, DateTime expiry, CancellationToken token);
	Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken token);
}
