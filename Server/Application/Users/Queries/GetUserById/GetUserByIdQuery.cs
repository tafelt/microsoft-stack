using Domain.Users;
using MediatR;

namespace Application.Users.Queries;

public class GetUserByIdQuery : IRequest<User>
{
  public int Id { get; set; }
}
