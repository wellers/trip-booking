using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TripBooking.Data.Entities;

namespace TripBooking.Data.Repositories;

public class TripRepository(BaseDbContext context) : ITripRepository
{
	private static IIncludableQueryable<Trip, ICollection<Registration>> AllTripsWithRegistrations(BaseDbContext context)
	{
		return context.Trips
			.Include(t => t.Registrations);
	}
	
	public async Task<List<Trip>> GetTripsAsync(CancellationToken token)
	{
		var list = await AllTripsWithRegistrations(context).ToListAsync(token);
		return list;
	}

	public async Task<Trip?> GetTripByIdAsync(int id, CancellationToken token) => await AllTripsWithRegistrations(context).SingleOrDefaultAsync(trip => trip.Id == id, token);
	
	public async Task<List<Trip>> GetTripsByCountryAsync(string country, CancellationToken token)
	{
		var list = await AllTripsWithRegistrations(context)
			.Where(trip => trip.Country == country)
			.ToListAsync(token);
		return list;
	}

	public async Task<Trip> AddTripAsync(Trip trip, CancellationToken token)
	{
		await context.Trips.AddAsync(trip, token);
		await context.SaveChangesAsync(token);
		return trip;
	}

	public async Task<bool> UpdateTripAsync(Trip trip, CancellationToken token)
	{
		var item = await GetTripByIdAsync(trip.Id, token);
		if (item is null)
			return false;

		item.Name = trip.Name;
		item.Description = trip.Description;
		item.Country = trip.Country;

		context.Update(item);
		var changes = await context.SaveChangesAsync(token);
		return changes > 0;
	}
	
	public async Task<bool> PatchTripAsync(Trip trip, CancellationToken token)
	{
		var item = await GetTripByIdAsync(trip.Id, token);
		if (item is null)
			return false;

		if (!string.IsNullOrEmpty(trip.Name))
			item.Name = trip.Name;

		if (!string.IsNullOrEmpty(trip.Description))
			item.Description = trip.Description;

		if (!string.IsNullOrEmpty(trip.Country))
			item.Country = trip.Country;

		context.Update(item);
		var changes = await context.SaveChangesAsync(token);
		return changes > 0;
	}
	
	public async Task<bool> DeleteTripAsync(int id, CancellationToken token)
	{
		var item = await GetTripByIdAsync(id, token);
		if (item is null)
			return false;
		
		context.Trips.Remove(item);
		var changes = await context.SaveChangesAsync(token);

		return changes > 0;
	}
}