using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Entities;

namespace TripBooking.Data;

public class BaseDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Trip> Trips { get; set; }
	public DbSet<Registration> Registrations { get; set; }
}