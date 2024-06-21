using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Dtos;

namespace TripBooking.Data;

public static class DataGenerator
{
	public static async void Initialise()
	{
		await using var context = new InMemoryDbContext();
		
		var trip = new Trip
		{
			Name = "Trip1",
			Description = "This is an awesome trip",
			Country = "United Kingdom"
		};
		
		await context.Trips.AddRangeAsync(trip);
		await context.SaveChangesAsync();
		
		var registration = new Registration
		{
			FullName = "Testy McTest",
			TripId = trip.Id,
			Trip = trip
		};

		await context.Registrations.AddRangeAsync(registration);
		await context.SaveChangesAsync();
	}
}