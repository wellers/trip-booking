using TripBooking.Data.Entities;

namespace TripBooking.Data.Repositories;

public interface ITripRepository
{
	Task<List<Trip>> GetTripsAsync(CancellationToken token);
	Task<Trip?> GetTripByIdAsync(int id, CancellationToken token);
	Task<List<Trip>> GetTripsByCountryAsync(string country, CancellationToken token);
	Task<Trip> AddTripAsync(Trip trip, CancellationToken token);
	Task<bool> UpdateTripAsync(Trip trip, CancellationToken token);
	Task<bool> PatchTripAsync(Trip trip, CancellationToken token);
	Task<bool> DeleteTripAsync(int id, CancellationToken token);
}