using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Entities;

namespace TripBooking.Data.Repositories;

public class UserRepository(BaseDbContext context) : IUserRepository
{
	public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken token) => await context.Users.SingleOrDefaultAsync(x => x.Username == username, token);
}
