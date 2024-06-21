using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Dtos;

namespace TripBooking.Data;

public class InMemoryDbContext : DbContext
{
	public DbSet<Trip> Trips { get; set; }
	public DbSet<Registration> Registrations { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase(databaseName: "TripBookingDb");
	}

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