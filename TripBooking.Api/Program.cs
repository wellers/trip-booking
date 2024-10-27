using Microsoft.EntityFrameworkCore;
using TripBooking.Api.Exceptions;
using TripBooking.Data;
using TripBooking.Data.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("TripBookingDb"));

// Add services to the container.
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
	await DataGenerator.InitialiseAsync(context);
}

app.Run();