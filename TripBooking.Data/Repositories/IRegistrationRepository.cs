using TripBooking.Data.Dtos;

namespace TripBooking.Data.Repositories;

public interface IRegistrationRepository
{
	public Task<List<Registration>> GetRegistrationsAsync();
	public Task<bool> AddRegistrationAsync(Registration registration);
}