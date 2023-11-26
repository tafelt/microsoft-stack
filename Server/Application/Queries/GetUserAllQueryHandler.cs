using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

internal class GetUserAllQueryHandler : IRequestHandler<GetUserAllQuery, List<User>>
{
  private readonly IUserRepository _userRepository;

  public GetUserAllQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<List<User>> Handle(GetUserAllQuery request, CancellationToken cancellationToken)
  {
    return _userRepository.GetAllAsync();
  }
}
