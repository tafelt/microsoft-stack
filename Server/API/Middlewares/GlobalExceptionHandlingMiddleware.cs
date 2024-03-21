using System.Net;
using System.Text.Json;
using Domain.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
  private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

  public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) =>
    _logger = logger;

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    async Task WriteExceptionResponseAsync<T>(HttpStatusCode statusCode, T problemDetails)
    {
      context.Response.StatusCode = (int)statusCode;
      context.Response.ContentType = "application/json";

      var json = JsonSerializer.Serialize(problemDetails);

      await context.Response.WriteAsync(json);
    }

    try
    {
      await next(context);
    }
    catch (ValidationException e)
    {
      _logger.LogError(e, e.Message);

      await WriteExceptionResponseAsync(HttpStatusCode.BadRequest, e.Errors);
    }
    catch (Exception e) when (e is IDomainException ex)
    {
      _logger.LogError(e, ex.Detail);

      var problemDetails = new ProblemDetails
      {
        Status = (int)ex.StatusCode,
        Type = ex.Type.Name,
        Title = ex.Title,
        Detail = ex.Detail
      };

      await WriteExceptionResponseAsync(ex.StatusCode, problemDetails);
    }
    catch (Exception e)
    {
      _logger.LogError(e, e.Message);

      var problemDetails = new ProblemDetails
      {
        Status = (int)HttpStatusCode.InternalServerError,
        Type = typeof(Exception).Name,
        Title = "InternalServerError",
        Detail = e.Message
      };

      await WriteExceptionResponseAsync(HttpStatusCode.InternalServerError, problemDetails);
    }
  }
}
