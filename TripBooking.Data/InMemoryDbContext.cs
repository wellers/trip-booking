using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Dtos;

namespace TripBooking.Data;

public class InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : BaseDbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		
		modelBuilder.Entity<Trip>()
			.HasMany(t => t.Registrations)
			.WithOne(r => r.Trip)
			.HasForeignKey(r => r.TripId);
		
		modelBuilder.Entity<Trip>()
			.Property(t => t.Id)
			.ValueGeneratedOnAdd();

		modelBuilder.Entity<Registration>()
			.Property(r => r.Id)
			.ValueGeneratedOnAdd();
	}
}