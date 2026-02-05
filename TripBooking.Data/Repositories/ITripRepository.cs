using TripBooking.Data.Dtos;

namespace TripBooking.Data.Repositories;

public interface ITripRepository
{
	public Task<List<Trip>> GetTripsAsync(CancellationToken token);
	public Task<Trip?> GetTripByIdAsync(int id, CancellationToken token);
	public Task<List<Trip>> GetTripsByCountryAsync(string country, CancellationToken token);
	public Task<Trip> AddTripAsync(Trip trip, CancellationToken token);
	public Task<bool> UpdateTripAsync(Trip trip, CancellationToken token);
	public Task<bool> PatchTripAsync(Trip trip, CancellationToken token);
	public Task<bool> DeleteTripAsync(int id, CancellationToken token);
}