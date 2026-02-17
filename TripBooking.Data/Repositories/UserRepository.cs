using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Entities;

namespace TripBooking.Data.Repositories;

public class UserRepository(BaseDbContext context) : IUserRepository
{
	public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken token) => await context.Users.SingleOrDefaultAsync(x => x.Username == username, token);

	public async Task SetRefreshTokenAsync(int userId, string refreshToken, DateTime expiry, CancellationToken token)
	{
		var user = await context.Users.FindAsync(new object[] { userId }, token);
		
		if (user == null) 
			return;

		user.RefreshToken = refreshToken;
		user.RefreshTokenExpiry = expiry;
		await context.SaveChangesAsync(token);
	}

	public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken token) => await context.Users.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken, token);
}
