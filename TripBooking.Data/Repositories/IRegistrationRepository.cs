using TripBooking.Data.Entities;

namespace TripBooking.Data.Repositories;

public interface IRegistrationRepository
{
	Task<List<Registration>> GetRegistrationsAsync(CancellationToken token);
	Task<Registration> AddRegistrationAsync(Registration registration, CancellationToken token);
	Task<Registration?> GetRegistrationByIdAsync(int id, CancellationToken token);
}