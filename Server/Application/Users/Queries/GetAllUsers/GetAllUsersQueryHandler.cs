using Domain.Users;
using MediatR;

namespace Application.Users.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
  private readonly IUserRepository _userRepository;

  public GetAllUsersQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<IEnumerable<User>> Handle(
    GetAllUsersQuery request,
    CancellationToken cancellationToken
  )
  {
    return _userRepository.GetAllAsync();
  }
}
