namespace TripBooking.Shared;

public class TripResponse
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? Country { get; set; }
	public List<RegistrationResponse> Registrations { get; set; } = [];

	public static TripResponse FromDto(Data.Dtos.Trip dto) => new()
	{
		Id = dto.Id,
		Name = dto.Name,
		Description = dto.Description,
		Registrations = dto.Registrations.Select(RegistrationResponse.FromDto).ToList(),
		Country = dto.Country
	};
}