using Application.Users.Commands;
using Application.Users.Queries;
using Domain.Entities;
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
  public Task<IEnumerable<User>> GetUsers()
  {
    var query = new GetUserAllQuery();
    var result = _mediator.Send(query);

    return result;
  }

  [HttpGet("{id:int}")]
  public Task<User?> GetUserById(int id)
  {
    var query = new GetUserByIdQuery { Id = id };
    var result = _mediator.Send(query);

    return result;
  }

  [HttpPost]
  public Task<User> CreateUser(User user)
  {
    var command = new CreateUserCommand { Name = user.Name, Email = user.Email };
    var result = _mediator.Send(command);

    return result;
  }

  [HttpPut("{id:int}")]
  public Task<User?> UpdateUser(int id, User user)
  {
    var command = new UpdateUserCommand
    {
      Id = id,
      Name = user.Name,
      Email = user.Email
    };

    var result = _mediator.Send(command);

    return result;
  }

  [HttpDelete("{id:int}")]
  public Task<User?> DeleteUser(int id)
  {
    var command = new DeleteUserCommand { Id = id };
    var result = _mediator.Send(command);

    return result;
  }
}
