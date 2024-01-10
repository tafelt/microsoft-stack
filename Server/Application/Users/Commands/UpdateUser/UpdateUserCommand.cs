using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public class UpdateUserCommand : IRequest<User?>
{
  public int Id { get; set; }

  public required string Name { get; set; }

  public required string Email { get; set; }
}
