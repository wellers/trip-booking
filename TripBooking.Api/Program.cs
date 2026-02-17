using Microsoft.EntityFrameworkCore;
using TripBooking.Api.Exceptions;
using TripBooking.Data;
using TripBooking.Data.Repositories;
using Scalar.AspNetCore;
using TripBooking.Business.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var secret = builder.Configuration["Jwt:Secret"] ?? throw new ArgumentNullException("JWT Secret");
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
		};
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("WriteClaim", policy => policy.RequireClaim("perms", "write"));
});

// Swap for actual SQL database
builder.Services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("TripBookingDb"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
	options.DefaultApiVersion = new(1, 0);
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.ReportApiVersions = true;
}).AddMvc();

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
app.UseAuthentication();
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