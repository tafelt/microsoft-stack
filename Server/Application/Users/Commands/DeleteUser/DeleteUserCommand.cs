using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public class DeleteUserCommand : IRequest<User?>
{
  public int Id { get; set; }
}
