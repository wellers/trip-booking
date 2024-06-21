using Microsoft.AspNetCore.Mvc;
using TripBooking.Data.Repositories;
using TripBooking.Models;

namespace TripBooking.Controllers
{
	[ApiController]
	[Route("/api/v1/trips")]
	public class TripController(ITripRepository tripRepository) : ControllerBase
	{
		[HttpPost]
		public async ValueTask<IActionResult?> CreateAsync(Trip trip)
		{
			var result = await tripRepository.AddTripAsync(new Dtos.Trip
			{
				Name = trip.Name, 
				Description = trip.Description, 
				Country = trip.Country
			});
			
			var message = result
				? "Trip successfully created."
				: "Trip failed to create";
			
			return Ok(message);
		}
		
		[HttpPut("{id:}")]
		public async ValueTask<IActionResult?> UpdateAsync(int id, [FromBody] Trip trip)
		{
			var result = await tripRepository.UpdateTripAsync(new Dtos.Trip
			{
				Id = id, 
				Name = trip.Name, 
				Description = trip.Description, 
				Country = trip.Country
			});
			
			var message = result
				? "Trip successfully updated."
				: "Trip failed to update";
			
			return Ok(message);
		}
		
		[HttpDelete("{id:}")]
		public async ValueTask<IActionResult?> DeleteAsync(int id)
		{
			var result = await tripRepository.DeleteTripAsync(id);

			var message = result
				? "Trip successfully deleted."
				: "Trip failed to delete";
			
			return Ok(message);
		}
		
		[HttpGet("{id:int}")]
		public async ValueTask<IActionResult?> GetByIdAsync(int id)
		{
			var item = await tripRepository.GetTripByIdAsync(id);
			
			return Ok(
				item
			);
		}
		
		[HttpGet("{country}")]
		public async ValueTask<IActionResult?> GetByCountryAsync(string country)
		{
			var list = await tripRepository.GetTripsByCountryAsync(country);
			
			return Ok(
				list
			);
		}
		
		[HttpGet]
		public async ValueTask<IActionResult?> GetAllAsync()
		{
			var list = await tripRepository.GetTripsAsync();
			
			return Ok(
				list
			);
		}
	}
}