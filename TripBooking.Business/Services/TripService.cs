using TripBooking.Data.Repositories;

namespace TripBooking.Business.Services
{
	public class TripService(ITripRepository tripRepository) : ITripService
	{
		public async Task<Shared.Response.Trip?> CreateTripAsync(Shared.Dtos.Trip trip, CancellationToken token)
		{
			var created = await tripRepository.AddTripAsync(new Data.Entities.Trip
			{
				Name = trip.Name,
				Description = trip.Description,
				Country = trip.Country
			}, token);

			return created is null ? null : Shared.Response.Trip.FromDto(created);
		}

		public async Task<bool> UpdateTripAsync(int id, Shared.Dtos.Trip trip, CancellationToken token) => await tripRepository.UpdateTripAsync(new Data.Entities.Trip
		{
			Id = id,
			Name = trip.Name,
			Description = trip.Description,
			Country = trip.Country
		}, token);

		public async Task<bool> PatchTripAsync(int id, Shared.Dtos.Trip trip, CancellationToken token) => await tripRepository.PatchTripAsync(new Data.Entities.Trip
		{
			Id = id,
			Name = trip.Name,
			Description = trip.Description,
			Country = trip.Country
		}, token);

		public async Task<bool> DeleteTripAsync(int id, CancellationToken token) => await tripRepository.DeleteTripAsync(id, token);

		public async Task<Shared.Response.Trip?> GetTripByIdAsync(int id, CancellationToken token)
		{
			var trip = await tripRepository.GetTripByIdAsync(id, token);
			return trip is null ? null : Shared.Response.Trip.FromDto(trip);
		}

		public async Task<List<Shared.Response.Trip>> GetTripsByCountryAsync(string country, CancellationToken token)
		{
			var list = await tripRepository.GetTripsByCountryAsync(country, token);
			return list.Select(Shared.Response.Trip.FromDto).ToList();
		}

		public async Task<List<Shared.Response.Trip>> GetTripsAsync(CancellationToken token)
		{
			var list = await tripRepository.GetTripsAsync(token);
			return list.Select(Shared.Response.Trip.FromDto).ToList();
		}
	}
}