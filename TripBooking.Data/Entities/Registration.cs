namespace TripBooking.Data.Entities;

public class Registration
{
	public int Id { get; set; }
	public string FullName { get; set; }
	public int TripId { get; set; }
	public Trip Trip { get; set; }
}