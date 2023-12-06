using Application.Users.Queries;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Users.QueryHandlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
{
  private readonly IUserRepository _userRepository;

  public GetUserByIdQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
  {
    return _userRepository.GetByIdAsync(request.Id);
  }
}
