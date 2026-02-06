namespace TripBooking.Shared;

public class RegistrationResponse
{
	public int Id { get; set; }
	public string? FullName { get; set; }

	public static RegistrationResponse FromDto(Data.Dtos.Registration dto) => new()
	{
		Id = dto.Id,
		FullName = dto.FullName
	};
}