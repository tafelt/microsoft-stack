using Application.Users.Commands;
using Application.Users.Queries;
using Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
  private readonly IMediator _mediator;

  public UsersController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<IEnumerable<UserResponse>> GetUsers()
  {
    var query = new GetAllUsersQuery();
    var result = await _mediator.Send(query);

    return result.Select(user => new UserResponse(user.Id, user.Name, user.Email));
  }

  [HttpGet("{id:int}")]
  public async Task<UserResponse?> GetUserById(int id)
  {
    var query = new GetUserByIdQuery { Id = id };
    var result = await _mediator.Send(query);

    if (result is null)
    {
      return null;
    }

    return new UserResponse(result.Id, result.Name, result.Email);
  }

  [HttpPost]
  public async Task<UserResponse> CreateUser(CreateUserRequest request)
  {
    var command = new CreateUserCommand { Name = request.Name, Email = request.Email };
    var result = await _mediator.Send(command);

    return new UserResponse(result.Id, result.Name, result.Email);
  }

  [HttpPut("{id:int}")]
  public async Task<UserResponse?> UpdateUser(int id, UpdateUserRequest request)
  {
    var command = new UpdateUserCommand
    {
      Id = id,
      Name = request.Name,
      Email = request.Email
    };

    var result = await _mediator.Send(command);

    if (result is null)
    {
      return null;
    }

    return new UserResponse(result.Id, result.Name, result.Email);
  }

  [HttpDelete("{id:int}")]
  public async Task<UserResponse?> DeleteUser(int id)
  {
    var command = new DeleteUserCommand { Id = id };
    var result = await _mediator.Send(command);

    if (result is null)
    {
      return null;
    }

    return new UserResponse(result.Id, result.Name, result.Email);
  }
}
