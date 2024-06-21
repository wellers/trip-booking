using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Dtos;

namespace TripBooking.Data.Repositories;

public class TripRepository : ITripRepository
{
	public async Task<List<Trip>> GetTripsAsync()
	{
		await using var context = new InMemoryDbContext();
		var list = await context.Trips
			.Include(t => t.Registrations)
			.ToListAsync();
		return list;
	}

	public async Task<Trip?> GetTripByIdAsync(int id)
	{
		await using var context = new InMemoryDbContext();
		var item = await context.Trips
			.Include(t => t.Registrations)
			.SingleOrDefaultAsync(trip => trip.Id == id);
		return item;
	}
	
	public async Task<List<Trip>> GetTripsByCountryAsync(string country)
	{
		await using var context = new InMemoryDbContext();
		var list = await context.Trips
			.Include(trip => trip.Registrations)
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