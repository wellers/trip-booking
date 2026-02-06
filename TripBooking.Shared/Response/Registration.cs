namespace TripBooking.Shared.Response;

public class Registration
{
	public int Id { get; set; }
	public string? FullName { get; set; }

	public static Registration FromDto(Data.Entities.Registration dto) => new()
	{
		Id = dto.Id,
		FullName = dto.FullName
	};
}