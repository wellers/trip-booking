namespace TripBooking.Business.Services;

public interface IRegistrationService
{
	Task<Shared.Response.Registration?> CreateRegistrationAsync(Shared.Request.Registration registration, CancellationToken token);
	Task<IReadOnlyList<Shared.Response.Registration>> GetRegistrationsAsync(CancellationToken token);
	Task<Shared.Response.Registration?> GetRegistrationByIdAsync(int id, CancellationToken token);
}	
