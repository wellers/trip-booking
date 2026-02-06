using TripBooking.Data.Repositories;
using TripBooking.Shared;

namespace TripBooking.Business.Services
{
	public class TripService(ITripRepository tripRepository) : ITripService
	{
		public async Task<TripResponse?> CreateTripAsync(Trip trip, CancellationToken token)
		{
			var created = await tripRepository.AddTripAsync(new Data.Dtos.Trip
			{
				Name = trip.Name,
				Description = trip.Description,
				Country = trip.Country
			}, token);

			return created is null ? null : TripResponse.FromDto(created);
		}

		public async Task<bool> UpdateTripAsync(int id, Trip trip, CancellationToken token) => await tripRepository.UpdateTripAsync(new Data.Dtos.Trip
		{
			Id = id,
			Name = trip.Name,
			Description = trip.Description,
			Country = trip.Country
		}, token);

		public async Task<bool> PatchTripAsync(int id, Trip trip, CancellationToken token) => await tripRepository.PatchTripAsync(new Data.Dtos.Trip
		{
			Id = id,
			Name = trip.Name,
			Description = trip.Description,
			Country = trip.Country
		}, token);

		public async Task<bool> DeleteTripAsync(int id, CancellationToken token) => await tripRepository.DeleteTripAsync(id, token);

		public async Task<TripResponse?> GetTripByIdAsync(int id, CancellationToken token)
		{
			var trip = await tripRepository.GetTripByIdAsync(id, token);
			return trip is null ? null : TripResponse.FromDto(trip);
		}

		public async Task<List<TripResponse>> GetTripsByCountryAsync(string country, CancellationToken token)
		{
			var list = await tripRepository.GetTripsByCountryAsync(country, token);
			return list.Select(TripResponse.FromDto).ToList();
		}

		public async Task<List<TripResponse>> GetTripsAsync(CancellationToken token)
		{
			var list = await tripRepository.GetTripsAsync(token);
			return list.Select(TripResponse.FromDto).ToList();
		}
	}
}