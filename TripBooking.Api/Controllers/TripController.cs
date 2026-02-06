using Microsoft.AspNetCore.Mvc;
using TripBooking.Business.Services;
using TripBooking.Shared.Request;

namespace TripBooking.Api.Controllers;

[ApiController]
[Route("api/v1/trips")]
public class TripController(ITripService tripService) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] Trip trip, CancellationToken token)
	{
		var created = await tripService.CreateTripAsync(trip, token);

		if (created is null)
			return BadRequest("Trip failed to create.");

		return CreatedAtRoute("GetTripById", new { id = created.Id }, created);
	}
		
	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateAsync(int id, [FromBody] Trip trip, CancellationToken token)
	{
		if (trip is null) 
			return BadRequest();

		var updated = await tripService.UpdateTripAsync(id, trip, token);
			
		if (!updated) 
			return NotFound();

		return NoContent();
	}

	[HttpPatch("{id:int}")]
	public async Task<IActionResult> PatchAsync(int id, [FromBody] Trip trip, CancellationToken token)
	{
		if (trip is null)
			return BadRequest();

		var patched = await tripService.PatchTripAsync(id, trip, token);

		if (!patched)
			return NotFound();

		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
	{
		var deleted = await tripService.DeleteTripAsync(id, token);

		if (!deleted)
			return NotFound();
			
		return NoContent();
	}
		
	[HttpGet("{id:int}", Name = "GetTripById")]
	public async Task<IActionResult> GetByIdAsync(int id, CancellationToken token = default)
	{
		var trip = await tripService.GetTripByIdAsync(id, token);

		if (trip is null)
			return NotFound();
		
		return Ok(trip);
	}
		
	[HttpGet("{country}")]
	public async Task<IActionResult> GetByCountryAsync(string country, CancellationToken token)
	{
		var trips = await tripService.GetTripsByCountryAsync(country, token);
		return Ok(trips);
	}
		
	[HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken token)
	{
		var trips = await tripService.GetTripsAsync(token);
		return Ok(trips);
	}
}