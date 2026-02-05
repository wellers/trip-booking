using Microsoft.AspNetCore.Mvc;
using TripBooking.Api.Models;
using TripBooking.Data.Repositories;

namespace TripBooking.Api.Controllers;

[ApiController]
[Route("api/v1/register")]
public class RegisterController(ITripRepository tripRepository, IRegistrationRepository registrationRepository) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] Registration registration, CancellationToken token)
	{
		var trip = await tripRepository.GetTripByIdAsync(registration.TripId, token);

		if (trip is null)
			return NotFound();

		var newRegistration = new Data.Dtos.Registration
		{
			FullName = registration.FullName,
			Trip = trip
		};
		
		var created = await registrationRepository.AddRegistrationAsync(newRegistration, token);

		if (created is null)
			return BadRequest();

		return CreatedAtRoute("GetRegistrationById", new { id = created.Id }, RegistrationResponse.FromDto(created));
	}
	
	[HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken token)
	{
		var list = await registrationRepository.GetRegistrationsAsync(token);
			
		return Ok(list.Select(RegistrationResponse.FromDto));
	}

	[HttpGet("{id:int}", Name = "GetRegistrationById")]
	public async Task<IActionResult> GetByIdAsync(int id, CancellationToken token = default)
	{
		var item = await registrationRepository.GetRegistrationsByIdAsync(id, token);

		if (item is null)
			return NotFound();

		return Ok(RegistrationResponse.FromDto(item));
	}
}