using TripBooking.Data.Entities;

namespace TripBooking.Business.Services;

public interface IUserService
{
	Task<User?> Authenticate(string username, string password, CancellationToken token);
}
