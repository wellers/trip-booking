using Moq;
using TripBooking.Business.Services;
using TripBooking.Data.Entities;
using TripBooking.Data.Repositories;
using Xunit;

namespace TripBooking.Tests.Services;

public class RegistrationServiceTests
{
	[Fact]
	public async Task CreateRegistration_ReturnsNull_WhenTripNotFound()
	{
		var mockTripRepo = new Mock<ITripRepository>();
		mockTripRepo.Setup(x => x.GetTripByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync((Trip?)null);

		var mockRegRepo = new Mock<IRegistrationRepository>();

		var service = new RegistrationService(mockRegRepo.Object, mockTripRepo.Object);

		var result = await service.CreateRegistrationAsync(new Shared.Request.Registration { FullName = "Alice", TripId = 1 }, CancellationToken.None);

		Assert.Null(result);
	}

	[Fact]
	public async Task CreateRegistration_ReturnsResponse_WhenTripExists()
	{
		var trip = new Trip { Id = 5, Name = "T1" };

		var mockTripRepo = new Mock<ITripRepository>();
		mockTripRepo.Setup(x => x.GetTripByIdAsync(5, It.IsAny<CancellationToken>())).ReturnsAsync(trip);

		var created = new Registration { Id = 7, FullName = "Bob", Trip = trip };
		var mockRegRepo = new Mock<IRegistrationRepository>();
		mockRegRepo.Setup(x => x.AddRegistrationAsync(It.IsAny<Registration>(), It.IsAny<CancellationToken>())).ReturnsAsync(created);

		var service = new RegistrationService(mockRegRepo.Object, mockTripRepo.Object);

		var result = await service.CreateRegistrationAsync(new Shared.Request.Registration { FullName = "Bob", TripId = 5 }, CancellationToken.None);

		Assert.NotNull(result);
		Assert.Equal(7, result!.Id);
		Assert.Equal("Bob", result.FullName);
	}
}
