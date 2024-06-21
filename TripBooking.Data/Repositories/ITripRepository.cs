using TripBooking.Dtos;

namespace TripBooking.Data.Repositories;

public interface ITripRepository
{
	public Task<List<Trip>> GetTripsAsync();
	public Task<Trip?> GetTripByIdAsync(int id);
	public Task<List<Trip>> GetTripsByCountryAsync(string country);
	public Task<bool> AddTripAsync(Trip trip);
	public Task<bool> UpdateTripAsync(Trip trip);
	public Task<bool> DeleteTripAsync(int id);
}