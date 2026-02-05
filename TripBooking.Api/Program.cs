using Microsoft.EntityFrameworkCore;
using TripBooking.Api.Exceptions;
using TripBooking.Data;
using TripBooking.Data.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Swap for actual SQL database
builder.Services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("TripBookingDb"));

builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger(options =>
	{
		options.RouteTemplate = "openapi/{documentName}.json";
	});
	app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

#if DEBUG
// Set-up test data
using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
	await DataGenerator.InitialiseAsync(context);
}
#endif

app.Run();