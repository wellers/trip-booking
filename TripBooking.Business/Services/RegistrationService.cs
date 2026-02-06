using TripBooking.Data.Repositories;
using TripBooking.Shared;

namespace TripBooking.Business.Services
{
	public class RegistrationService(IRegistrationRepository registrationRepository, ITripRepository tripRepository) : IRegistrationService
	{
		public async Task<RegistrationResponse?> CreateRegistrationAsync(Registration registration, CancellationToken token)
		{
			var trip = await tripRepository.GetTripByIdAsync(registration.TripId, token);

			if (trip is null)
				return null;

			var newRegistration = new Data.Dtos.Registration
			{
				FullName = registration.FullName,
				Trip = trip
			};

			var created = await registrationRepository.AddRegistrationAsync(newRegistration, token);

			return RegistrationResponse.FromDto(created);
		}

		public async Task<RegistrationResponse?> GetRegistrationByIdAsync(int id, CancellationToken token)
		{
			var item = await registrationRepository.GetRegistrationByIdAsync(id, token);
			return item is null ? null : RegistrationResponse.FromDto(item);
		}

		public async Task<List<RegistrationResponse>> GetRegistrationsAsync(CancellationToken token)
		{
			var list = await registrationRepository.GetRegistrationsAsync(token);
			return list.Select(RegistrationResponse.FromDto).ToList();
		}
	}
}
