using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Dtos;

namespace TripBooking.Data.Repositories;

public class RegistrationRepository(BaseDbContext context) : IRegistrationRepository
{
	public async Task<List<Registration>> GetRegistrationsAsync()
	{
		var list = await context.Registrations
			.Include(r => r.Trip)
			.ToListAsync();
		return list;
	}
	
	public async Task<bool> AddRegistrationAsync(Registration registration)
	{
		await context.Registrations.AddAsync(registration);
		
		var changes = await context.SaveChangesAsync();
		return changes > 0;
	}
}