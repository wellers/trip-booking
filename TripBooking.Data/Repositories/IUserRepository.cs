using TripBooking.Data.Entities;

namespace TripBooking.Data.Repositories;

public interface IUserRepository
{
	Task<User?> GetUserByUsernameAsync(string username, CancellationToken token);
}
