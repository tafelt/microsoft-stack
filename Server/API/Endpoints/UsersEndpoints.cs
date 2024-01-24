using Application.Users.Commands;
using Application.Users.Queries;
using Carter;
using Contracts.Users;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public class UsersEndpoints : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/users");

    group.MapPost("", CreateUser).WithName(nameof(CreateUser));
    group.MapGet("", GetUsers).WithName(nameof(GetUsers));
    group.MapGet("{id:int}", GetUser).WithName(nameof(GetUser));
    group.MapPut("{id:int}", UpdateUser).WithName(nameof(UpdateUser));
    group.MapDelete("{id:int}", DeleteUser).WithName(nameof(DeleteUser));
  }

  public static async Task<
    Results<Ok<UserResponse>, BadRequest<IEnumerable<ValidationFailure>>, Conflict<ProblemDetails>>
  > CreateUser([FromBody] CreateUserRequest request, ISender sender)
  {
    var command = request.Adapt<CreateUserCommand>();
    var result = await sender.Send(command);
    var response = result.Adapt<UserResponse>();

    return TypedResults.Ok(response);
  }

  public static async Task<Ok<IEnumerable<UserResponse>>> GetUsers(ISender sender)
  {
    var query = new GetAllUsersQuery();
    var result = await sender.Send(query);
    var response = result.Adapt<IEnumerable<UserResponse>>();

    return TypedResults.Ok(response);
  }

  public static async Task<Results<Ok<UserResponse>, NotFound<ProblemDetails>>> GetUser(
    int id,
    ISender sender
  )
  {
    var query = new GetUserByIdQuery { Id = id };
    var result = await sender.Send(query);
    var response = result.Adapt<UserResponse>();

    return TypedResults.Ok(response);
  }

  public static async Task<
    Results<Ok<UserResponse>, BadRequest<IEnumerable<ValidationFailure>>, NotFound<ProblemDetails>>
  > UpdateUser(int id, [FromBody] UpdateUserRequest request, ISender sender)
  {
    var command = new UpdateUserCommand
    {
      Id = id,
      Name = request.Name,
      Email = request.Email
    };

    var result = await sender.Send(command);
    var response = result.Adapt<UserResponse>();

    return TypedResults.Ok(response);
  }

  public static async Task<Results<Ok<UserResponse>, NotFound<ProblemDetails>>> DeleteUser(
    int id,
    ISender sender
  )
  {
    var command = new DeleteUserCommand { Id = id };
    var result = await sender.Send(command);
    var response = result.Adapt<UserResponse>();

    return TypedResults.Ok(response);
  }
}
