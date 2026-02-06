namespace TripBooking.Business.Services
{
	public interface IRegistrationService
	{
		Task<Shared.Response.Registration?> CreateRegistrationAsync(Shared.Dtos.Registration registration, CancellationToken token);
		Task<List<Shared.Response.Registration>> GetRegistrationsAsync(CancellationToken token);
		Task<Shared.Response.Registration?> GetRegistrationByIdAsync(int id, CancellationToken token);
	}	
}
