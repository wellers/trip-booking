using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TripBooking.Data.Dtos;

namespace TripBooking.Data.Repositories;

public class TripRepository : ITripRepository
{
	private static IIncludableQueryable<Trip, ICollection<Registration>> AllTripsWithRegistrations(InMemoryDbContext context)
	{
		return context.Trips
			.Include(t => t.Registrations);
	}
	
	public async Task<List<Trip>> GetTripsAsync()
	{
		await using var context = new InMemoryDbContext();
		var list = await AllTripsWithRegistrations(context).ToListAsync();
		return list;
	}

	public async Task<Trip?> GetTripByIdAsync(int id)
	{
		await using var context = new InMemoryDbContext();
		var item = await AllTripsWithRegistrations(context).SingleOrDefaultAsync(trip => trip.Id == id);
		return item;
	}
	
	public async Task<List<Trip>> GetTripsByCountryAsync(string country)
	{
		await using var context = new InMemoryDbContext();
		var list = await AllTripsWithRegistrations(context)
			.Where(trip => trip.Country == country)
			.ToListAsync();
		return list;
	}

	public async Task<bool> AddTripAsync(Trip trip)
	{
		await using var context = new InMemoryDbContext();
		await context.Trips.AddAsync(trip);
		
		var changes = await context.SaveChangesAsync();
		return changes > 0;
	}

	public async Task<bool> UpdateTripAsync(Trip trip)
	{
		await using var context = new InMemoryDbContext();
		var item = await GetTripByIdAsync(trip.Id);
		if (item is null)
			return false;
		
		context.Update(trip);
		var changes = await context.SaveChangesAsync();
		
		return changes > 0;
	}
	
	public async Task<bool> DeleteTripAsync(int id)
	{
		await using var context = new InMemoryDbContext();
		var item = await GetTripByIdAsync(id);
		if (item is null)
			return false;
		
		context.Trips.Remove(item);
		var changes = await context.SaveChangesAsync();

		return changes > 0;
	}
}