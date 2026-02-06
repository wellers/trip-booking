namespace TripBooking.Shared.Response;

public class Trip
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? Country { get; set; }
	public List<Registration> Registrations { get; set; } = [];

	public static Trip FromDto(Data.Entities.Trip dto) => new()
	{
		Id = dto.Id,
		Name = dto.Name,
		Description = dto.Description,
		Registrations = dto.Registrations.Select(Registration.FromDto).ToList(),
		Country = dto.Country
	};
}