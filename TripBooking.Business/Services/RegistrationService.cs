using TripBooking.Data.Repositories;

namespace TripBooking.Business.Services
{
	public class RegistrationService(IRegistrationRepository registrationRepository, ITripRepository tripRepository) : IRegistrationService
	{
		public async Task<Shared.Response.Registration?> CreateRegistrationAsync(Shared.Dtos.Registration registration, CancellationToken token)
		{
			var trip = await tripRepository.GetTripByIdAsync(registration.TripId, token);

			if (trip is null)
				return null;

			var newRegistration = new Data.Entities.Registration
			{
				FullName = registration.FullName,
				Trip = trip
			};

			var created = await registrationRepository.AddRegistrationAsync(newRegistration, token);

			return Shared.Response.Registration.FromDto(created);
		}

		public async Task<Shared.Response.Registration?> GetRegistrationByIdAsync(int id, CancellationToken token)
		{
			var item = await registrationRepository.GetRegistrationByIdAsync(id, token);
			return item is null ? null : Shared.Response.Registration.FromDto(item);
		}

		public async Task<List<Shared.Response.Registration>> GetRegistrationsAsync(CancellationToken token)
		{
			var list = await registrationRepository.GetRegistrationsAsync(token);
			return list.Select(Shared.Response.Registration.FromDto).ToList();
		}
	}
}
