using Microsoft.AspNetCore.Mvc;
using TripBooking.Api.Models;
using TripBooking.Data.Repositories;

namespace TripBooking.Api.Controllers;

[ApiController]
[Route("/api/v1/register")]
public class RegisterController(ITripRepository tripRepository, IRegistrationRepository registrationRepository) : ControllerBase
{
	[HttpPost]
	public async ValueTask<IActionResult?> CreateAsync(Registration registration)
	{
		var trip = await tripRepository.GetTripByIdAsync(registration.TripId);

		if (trip is null)
			return NotFound();

		var newRegistration = new Data.Dtos.Registration
		{
			FullName = registration.FullName,
			Trip = trip
		};
		
		var result = await registrationRepository.AddRegistrationAsync(newRegistration);
		
		var message = result
			? "Registration successfully created."
			: "Registration failed to create";
			
		return Ok(message);
	}
	
	[HttpGet]
	public async ValueTask<IActionResult?> GetAllAsync()
	{
		var list = await registrationRepository.GetRegistrationsAsync();
			
		return Ok(list.Select(RegistrationResponse.FromDto));
	}
}