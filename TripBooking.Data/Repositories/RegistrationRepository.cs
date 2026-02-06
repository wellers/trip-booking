using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Entities;

namespace TripBooking.Data.Repositories;

public class RegistrationRepository(BaseDbContext context) : IRegistrationRepository
{
	public async Task<List<Registration>> GetRegistrationsAsync(CancellationToken token) => 
		await context.Registrations.Include(r => r.Trip).ToListAsync(token);

	public async Task<Registration> AddRegistrationAsync(Registration registration, CancellationToken token)
	{
		await context.Registrations.AddAsync(registration, token);
		await context.SaveChangesAsync(token);

		return registration;
	}

	public async Task<Registration?> GetRegistrationByIdAsync(int id, CancellationToken token) => await context.Registrations
		.Include(r => r.Trip)
		.SingleOrDefaultAsync(r => r.Id == id, token);
}