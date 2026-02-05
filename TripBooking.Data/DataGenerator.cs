using TripBooking.Data.Dtos;

namespace TripBooking.Data;

public static class DataGenerator
{
	public static async Task InitialiseAsync(BaseDbContext context)
	{
		await context.Database.EnsureCreatedAsync();
		
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