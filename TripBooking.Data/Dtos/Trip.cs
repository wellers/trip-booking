namespace TripBooking.Data.Dtos;

public class Trip
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? Country { get; set; }
	public ICollection<Registration> Registrations { get; set; } = [];
}