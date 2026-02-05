using Microsoft.AspNetCore.Mvc;
using TripBooking.Api.Models;
using TripBooking.Data.Repositories;

namespace TripBooking.Api.Controllers;

[ApiController]
[Route("api/v1/trips")]
public class TripController(ITripRepository tripRepository) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] Trip trip, CancellationToken token)
	{
		var created = await tripRepository.AddTripAsync(new Data.Dtos.Trip
		{
			Name = trip.Name, 
			Description = trip.Description, 
			Country = trip.Country
		}, token);

		if (created is null)
			return BadRequest("Trip failed to create.");

		return CreatedAtRoute("GetTripById", new { id = created.Id }, TripResponse.FromDto(created));
	}
		
	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateAsync(int id, [FromBody] Trip trip, CancellationToken token)
	{
		if (trip is null) 
			return BadRequest();

		var result = await tripRepository.PatchTripAsync(new Data.Dtos.Trip
		{
			Id = id, 
			Name = trip.Name, 
			Description = trip.Description, 
			Country = trip.Country
		}, token);
			
		if (!result) 
			return NotFound();

		return NoContent();
	}

	[HttpPatch("{id:int}")]
	public async Task<IActionResult> PatchAsync(int id, [FromBody] Trip trip, CancellationToken token)
	{
		if (trip is null)
			return BadRequest();

		var result = await tripRepository.PatchTripAsync(new Data.Dtos.Trip
		{
			Id = id,
			Name = trip.Name,
			Description = trip.Description,
			Country = trip.Country
		}, token);

		if (!result)
			return NotFound();

		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
	{
		var result = await tripRepository.DeleteTripAsync(id, token);

		if (!result)
			return NotFound();
			
		return NoContent();
	}
		
	[HttpGet("{id:int}", Name = "GetTripById")]
	public async Task<IActionResult> GetByIdAsync(int id, CancellationToken token = default)
	{
		var item = await tripRepository.GetTripByIdAsync(id, token);

		if (item is null)
			return NotFound();
		
		return Ok(TripResponse.FromDto(item));
	}
		
	[HttpGet("{country}")]
	public async Task<IActionResult> GetByCountryAsync(string country, CancellationToken token)
	{
		var list = await tripRepository.GetTripsByCountryAsync(country, token);
			
		return Ok(list.Select(TripResponse.FromDto));
	}
		
	[HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken token)
	{
		var list = await tripRepository.GetTripsAsync(token);
			
		return Ok(list.Select(TripResponse.FromDto));
	}
}