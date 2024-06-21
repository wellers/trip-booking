using Microsoft.EntityFrameworkCore;
using TripBooking.Dtos;

namespace TripBooking.Data;

public class TripsDbContext : DbContext
{
	public DbSet<Trip> Trips { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase(databaseName: "TripsDb");
	}
}