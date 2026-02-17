using TripBooking.Data.Entities;

namespace TripBooking.Data;

public static class DataGenerator
{
	public static async Task InitialiseAsync(BaseDbContext context)
	{
		await context.Database.EnsureCreatedAsync();

		// encrypting passwords is out of scope for this demo, but in a real application you should never store passwords in plain text
		var user1 = new User { Id = 1, Username = "user1", Password = "password1", Permissions = "write" };
		var user2 = new User { Id = 2, Username = "user2", Password = "password2", Permissions = "" };

		await context.Users.AddRangeAsync(user1, user2);
		await context.SaveChangesAsync();

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