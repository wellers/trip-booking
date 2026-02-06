using TripBooking.Shared;

namespace TripBooking.Business.Services
{
	public interface IRegistrationService
	{
		Task<RegistrationResponse?> CreateRegistrationAsync(Registration registration, CancellationToken token);
		Task<List<RegistrationResponse>> GetRegistrationsAsync(CancellationToken token);
		Task<RegistrationResponse?> GetRegistrationByIdAsync(int id, CancellationToken token);
	}	
}
