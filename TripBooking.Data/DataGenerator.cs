using TripBooking.Dtos;

namespace TripBooking.Data;

public class DataGenerator
{
	public static void Initialise(IServiceProvider serviceProvider)
	{
		using var context = new TripsDbContext();
		var trips = new List<Trip>
		{
			new()
			{
				Id = 1,
				Name = "Trip1",
				Description = "This is an awesome trip",
				Country = "United Kingdom"
			}
		};
		context.Trips.AddRangeAsync(trips);
		context.SaveChangesAsync();
	}
}