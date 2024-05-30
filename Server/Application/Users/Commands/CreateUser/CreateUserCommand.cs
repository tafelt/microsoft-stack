using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public class CreateUserCommand : IRequest<User>
{
  public required string Name { get; set; }

  public required string Email { get; set; }

  public required Address Address { get; set; }
}
