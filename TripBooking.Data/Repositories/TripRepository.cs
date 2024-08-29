using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TripBooking.Data.Dtos;

namespace TripBooking.Data.Repositories;

public class TripRepository(BaseDbContext context) : ITripRepository
{
	private static IIncludableQueryable<Trip, ICollection<Registration>> AllTripsWithRegistrations(BaseDbContext context)
	{
		return context.Trips
			.Include(t => t.Registrations);
	}
	
	public async Task<List<Trip>> GetTripsAsync()
	{
		var list = await AllTripsWithRegistrations(context).ToListAsync();
		return list;
	}

	public async Task<Trip?> GetTripByIdAsync(int id)
	{
		var item = await AllTripsWithRegistrations(context).SingleOrDefaultAsync(trip => trip.Id == id);
		return item;
	}
	
	public async Task<List<Trip>> GetTripsByCountryAsync(string country)
	{
		var list = await AllTripsWithRegistrations(context)
			.Where(trip => trip.Country == country)
			.ToListAsync();
		return list;
	}

	public async Task<bool> AddTripAsync(Trip trip)
	{
		await context.Trips.AddAsync(trip);
		
		var changes = await context.SaveChangesAsync();
		return changes > 0;
	}

	public async Task<bool> UpdateTripAsync(Trip trip)
	{
		var item = await GetTripByIdAsync(trip.Id);
		if (item is null)
			return false;
		
		context.Update(trip);
		var changes = await context.SaveChangesAsync();
		
		return changes > 0;
	}
	
	public async Task<bool> DeleteTripAsync(int id)
	{
		var item = await GetTripByIdAsync(id);
		if (item is null)
			return false;
		
		context.Trips.Remove(item);
		var changes = await context.SaveChangesAsync();

		return changes > 0;
	}
}