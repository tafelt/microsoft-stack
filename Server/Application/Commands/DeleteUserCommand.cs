using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class DeleteUserCommand : IRequest<User?>
{
  public int Id { get; set; }
}
