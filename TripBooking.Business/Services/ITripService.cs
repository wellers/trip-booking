using TripBooking.Shared;

namespace TripBooking.Business.Services
{
	public interface ITripService
	{
		Task<TripResponse?> CreateTripAsync(Trip trip, CancellationToken token);
		Task<bool> UpdateTripAsync(int id, Trip trip, CancellationToken token);
		Task<bool> PatchTripAsync(int id, Trip trip, CancellationToken token);
		Task<bool> DeleteTripAsync(int id, CancellationToken token);
		Task<TripResponse?> GetTripByIdAsync(int id, CancellationToken token);
		Task<List<TripResponse>> GetTripsByCountryAsync(string country, CancellationToken token);
		Task<List<TripResponse>> GetTripsAsync(CancellationToken token);
	}
}
