namespace TripBooking.Business.Services;

public interface ITripService
{
	Task<Shared.Response.Trip?> CreateTripAsync(Shared.Request.Trip trip, CancellationToken token);
	Task<bool> UpdateTripAsync(int id, Shared.Request.Trip trip, CancellationToken token);
	Task<bool> PatchTripAsync(int id, Shared.Request.Trip trip, CancellationToken token);
	Task<bool> DeleteTripAsync(int id, CancellationToken token);
	Task<Shared.Response.Trip?> GetTripByIdAsync(int id, CancellationToken token);
	Task<IEnumerable<Shared.Response.Trip>> GetTripsByCountryAsync(string country, CancellationToken token);
	Task<IEnumerable<Shared.Response.Trip>> GetTripsAsync(CancellationToken token);
}
