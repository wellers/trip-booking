using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TripBooking.Api.Exceptions;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
	public async Task Invoke(HttpContext httpContext)
	{
		try
		{
			await next(httpContext);
		}
			
		catch (ValidationException)
		{
			await SetBadRequest(httpContext, "There were validations errors in your request.");
			return;
		}
		catch (InvalidOperationException)
		{
			await SetErrorRequest(httpContext, "An internal server error has occurred.");
			throw;
		}
		catch
		{
			await SetErrorRequest(httpContext, "An internal server error has occurred.");
			throw;
		}
	}

	private static async Task SetBadRequest(HttpContext httpContext, string title, string? message = null)
	{
		await SetProblemDetailsResponse(httpContext, title, message, 400);
	}

	private static async Task SetUnauthorisedRequest(HttpContext httpContext, string title, string? message = null)
	{
		httpContext.Response.Headers.Append("www-authenticate", "Bearer");

		await SetProblemDetailsResponse(httpContext, title, message, 401);
	}

	private static async Task SetForbiddenRequest(HttpContext httpContext, string title, string? message = null)
	{
		await SetProblemDetailsResponse(httpContext, title, message, 403);
	}

	private static async Task SetErrorRequest(HttpContext httpContext, string title, string? message = null)
	{
		await SetProblemDetailsResponse(httpContext, title, message, 500);
	}

	private static async Task SetProblemDetailsResponse(HttpContext httpContext, string title, string? message, int statusCode)
	{
		httpContext.Response.StatusCode = statusCode;

		if (httpContext.Request.Headers.Accept.Contains("application/json"))
			httpContext.Response.ContentType = "application/problem+json";

		var details = new ProblemDetails
		{
			Instance = httpContext.Request.Path,
			Status = statusCode,
			Title = title,
			Detail = message
		};
		await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(details));
	}
}