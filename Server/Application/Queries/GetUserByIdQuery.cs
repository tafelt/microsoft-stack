using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetUserByIdQuery : IRequest<User>
{
  public int Id { get; set; }
}
