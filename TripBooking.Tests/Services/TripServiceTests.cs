using Moq;
using TripBooking.Business.Services;
using TripBooking.Data.Entities;
using TripBooking.Data.Repositories;
using Xunit;

namespace TripBooking.Tests.Services;

public class TripServiceTests
{
	[Fact]
	public async Task CreateTrip_ReturnsNull_WhenRepositoryReturnsNull()
	{
		var mockRepo = new Mock<ITripRepository>();
		mockRepo.Setup(x => x.AddTripAsync(It.IsAny<Trip>(), It.IsAny<CancellationToken>())).ReturnsAsync((Trip?)null);

		var service = new TripService(mockRepo.Object);

		var result = await service.CreateTripAsync(new Shared.Request.Trip { Name = "n", Description = "d", Country = "c" }, CancellationToken.None);

		Assert.Null(result);
	}

	[Fact]
	public async Task CreateTrip_ReturnsMappedTrip_WhenRepositoryCreates()
	{
		var dto = new Trip { Id = 10, Name = "Name", Description = "Desc", Country = "Country" };
		var mockRepo = new Mock<ITripRepository>();
		mockRepo.Setup(x => x.AddTripAsync(It.IsAny<Trip>(), It.IsAny<CancellationToken>())).ReturnsAsync(dto);

		var service = new TripService(mockRepo.Object);

		var result = await service.CreateTripAsync(new Shared.Request.Trip { Name = "Name", Description = "Desc", Country = "Country" }, CancellationToken.None);

		Assert.NotNull(result);
		Assert.Equal(10, result!.Id);
		Assert.Equal("Name", result.Name);
		Assert.Equal("Country", result.Country);
	}

	[Fact]
	public async Task GetTripsByCountry_MapsAllItems()
	{
		var list = new List<Trip>
		{
			new() { Id = 1, Name = "A", Description = "D", Country = "X" },
			new() { Id = 2, Name = "B", Description = "E", Country = "X" }
		};

		var mockRepo = new Mock<ITripRepository>();
		mockRepo.Setup(x => x.GetTripsByCountryAsync("X", It.IsAny<CancellationToken>())).ReturnsAsync(list.AsEnumerable());

		var service = new TripService(mockRepo.Object);

		var result = await service.GetTripsByCountryAsync("X", CancellationToken.None);

		Assert.Equal(2, result.Count());
		Assert.Contains(result, r => r.Id == 1 && r.Name == "A");
	}
}
