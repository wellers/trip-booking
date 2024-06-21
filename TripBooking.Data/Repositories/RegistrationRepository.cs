using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Dtos;

namespace TripBooking.Data.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
	public async Task<List<Registration>> GetRegistrationsAsync()
	{
		await using var context = new InMemoryDbContext();
		var list = await context.Registrations
			.Include(r => r.Trip)
			.ToListAsync();
		return list;
	}
	
	public async Task<bool> AddRegistrationAsync(Registration registration)
	{
		await using var context = new InMemoryDbContext();
		
		await context.Registrations.AddAsync(registration);
		
		var changes = await context.SaveChangesAsync();
		return changes > 0;
	}
}