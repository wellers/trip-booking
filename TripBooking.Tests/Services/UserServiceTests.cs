using Moq;
using TripBooking.Business.Services;
using TripBooking.Business.Utils;
using TripBooking.Data.Entities;
using TripBooking.Data.Repositories;
using Xunit;

namespace TripBooking.Tests.Services;

public class UserServiceTests
{
	[Fact]
	public async Task Authenticate_ReturnsNull_WhenUserMissing()
	{
		var mockRepo = new Mock<IUserRepository>();
		mockRepo.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

		var aervice = new UserService(mockRepo.Object);

		var result = await aervice.Authenticate("u", "p", CancellationToken.None);

		Assert.Null(result);
	}

	[Fact]
	public async Task Authenticate_ReturnsUser_WhenPasswordMatches()
	{
		var password = "secret";
		var hashed = CryptoUtils.HashPassword(password);

		var user = new User { Id = 2, Username = "bob", Password = hashed };

		var mockRepo = new Mock<IUserRepository>();
		mockRepo.Setup(x => x.GetUserByUsernameAsync("bob", It.IsAny<CancellationToken>())).ReturnsAsync(user);

		var service = new UserService(mockRepo.Object);

		var result = await service.Authenticate("bob", password, CancellationToken.None);

		Assert.NotNull(result);
		Assert.Equal(2, result!.Id);
	}

	[Fact]
	public async Task Authenticate_ReturnsNull_WhenPasswordDoesNotMatch()
	{
		var user = new User { Id = 3, Username = "sam", Password = CryptoUtils.HashPassword("right") };

		var mockRepo = new Mock<IUserRepository>();
		mockRepo.Setup(x => x.GetUserByUsernameAsync("sam", It.IsAny<CancellationToken>())).ReturnsAsync(user);

		var service = new UserService(mockRepo.Object);

		var result = await service.Authenticate("sam", "wrong", CancellationToken.None);

		Assert.Null(result);
	}

	[Fact]
	public async Task SetRefreshToken_CallsRepository_WithHashedToken()
	{
		var mockRepo = new Mock<IUserRepository>();

		var service = new UserService(mockRepo.Object);

		var token = "rftoken";
		var expiry = DateTime.UtcNow.AddDays(1);

		await service.SetRefreshTokenAsync(5, token, expiry, CancellationToken.None);

		var expectedHash = CryptoUtils.HashRefreshToken(token);
		mockRepo.Verify(x => x.SetRefreshTokenAsync(5, expectedHash, expiry, It.IsAny<CancellationToken>()), Times.Once);
	}
}
