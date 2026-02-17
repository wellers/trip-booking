using Microsoft.AspNetCore.Authorization;

namespace TripBooking.Api.Attributes;

public class WriteClaimRequiredAttribute : Attribute, IAuthorizeData
{
	private const string PolicyName = "WriteClaim";

	public WriteClaimRequiredAttribute()
	{
		Policy = PolicyName;
	}

	public string? Policy { get; set; }
	public string? Roles { get; set; }
	public string? AuthenticationSchemes { get; set; }
}
