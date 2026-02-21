using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripBooking.Api.Attributes;
using TripBooking.Business.Services;
using TripBooking.Shared.Request;

namespace TripBooking.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/register")]
[ApiVersion("1.0")]
[Authorize]
public class RegisterController(IRegistrationService registrationService) : ControllerBase
{
	[HttpPost]
	[WriteClaimRequired]
	public async Task<IActionResult> CreateAsync([FromBody] Registration registration, CancellationToken token)
	{
		var created = await registrationService.CreateRegistrationAsync(registration, token);

		if (created is null)
			return BadRequest(new { message = "Registration failed to create." });

		return CreatedAtRoute("GetRegistrationById", new { id = created.Id }, created);
	}
	
	[HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken token)
	{
		var registrations = await registrationService.GetRegistrationsAsync(token);

		return Ok(registrations);
	}

	[HttpGet("{id:int}", Name = "GetRegistrationById")]
	public async Task<IActionResult> GetByIdAsync(int id, CancellationToken token = default)
	{
		var registration = await registrationService.GetRegistrationByIdAsync(id, token);

		if (registration is null)
			return NotFound();

		return Ok(registration);
	}
}