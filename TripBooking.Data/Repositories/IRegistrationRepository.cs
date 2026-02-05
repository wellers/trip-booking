using TripBooking.Data.Dtos;

namespace TripBooking.Data.Repositories;

public interface IRegistrationRepository
{
	public Task<List<Registration>> GetRegistrationsAsync(CancellationToken token);
	public Task<Registration> AddRegistrationAsync(Registration registration, CancellationToken token);
	public Task<Registration?> GetRegistrationsByIdAsync(int id, CancellationToken token);
}