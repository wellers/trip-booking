using TripBooking.Data.Entities;

namespace TripBooking.Data;

public static class DataGenerator
{
	public static async Task InitialiseAsync(BaseDbContext context)
	{
		await context.Database.EnsureCreatedAsync();
		
		var user1 = new User { Id = 1, Username = "user1", Password = HashPassword("password1"), Permissions = "write" };
		var user2 = new User { Id = 2, Username = "user2", Password = HashPassword("password2"), Permissions = "" };

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

	private static string HashPassword(string password)
	{
		var salt = new byte[16];
		using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
		rng.GetBytes(salt);

		using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 100_000, System.Security.Cryptography.HashAlgorithmName.SHA256);
		var hash = pbkdf2.GetBytes(32);

		return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);
	}
}