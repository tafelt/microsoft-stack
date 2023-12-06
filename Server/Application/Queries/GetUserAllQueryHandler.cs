using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Queries;

internal class GetUserAllQueryHandler : IRequestHandler<GetUserAllQuery, IEnumerable<User>>
{
  private readonly IUserRepository _userRepository;

  public GetUserAllQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<IEnumerable<User>> Handle(
    GetUserAllQuery request,
    CancellationToken cancellationToken
  )
  {
    return _userRepository.GetAllAsync();
  }
}
