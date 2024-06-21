using Microsoft.AspNetCore.Mvc;
using TripBooking.Models;

namespace TripBooking.Controllers
{
	[ApiController]
	[Route("/api/v1/trips")]
	public class TripController(ILogger<TripController> logger) : ControllerBase
	{
		private readonly ILogger<TripController> _logger = logger;

		[HttpPost]
		public async ValueTask<IActionResult?> CreateAsync(Trip trip)
		{
			var result = await Task.Run(() => new Trip { Id = 1, Name = "Trip1", Description = "This is a great trip" });
			
			return Ok("Trip successfully created.");
		}
		
		[HttpPut("{id:}")]
		public async ValueTask<IActionResult?> UpdateAsync(int id, [FromBody] Trip trip)
		{
			var result = await Task.Run(() => new Trip { Id = 1, Name = "Trip1", Description = "This is a great trip" });
			
			return Ok("Trip successfully updated.");
		}
		
		[HttpDelete("{id:}")]
		public async ValueTask<IActionResult?> DeleteAsync(int id)
		{
			var result = await Task.Run(() => new Trip { Id = 1, Name = "Trip1", Description = "This is a great trip" });
			
			return Ok("Trip successfully deleted.");
		}
		
		[HttpGet("{id:int}")]
		public async ValueTask<IActionResult?> GetByIdAsync(int id)
		{
			var trip = await Task.Run(() => new Trip { Id = 1, Name = "Trip1", Description = "This is a great trip" });
			
			return Ok(
				trip
			);
		}
		
		[HttpGet("{country}")]
		public async ValueTask<IActionResult?> GetByCountryAsync(string country)
		{
			var trip = await Task.Run(() => new Trip { Id = 1, Name = "Trip1", Description = "This is a great trip" });
			
			return Ok(
				trip
			);
		}
		
		[HttpGet]
		public async ValueTask<IActionResult?> GetAllAsync()
		{
			var trips = await Task.Run(() => new List<Trip>
			{
				new() { Id = 1, Name = "Trip1", Description = "This is a great trip" }
			});
			
			return Ok(
				trips
			);
		}
	}
}
