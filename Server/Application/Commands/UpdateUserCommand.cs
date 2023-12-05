using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class UpdateUserCommand : IRequest<User?>
{
  public int Id { get; set; }

  public required string Name { get; set; }

  public required string Email { get; set; }
}
