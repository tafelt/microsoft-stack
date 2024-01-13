using Domain.Users;
using Domain.Users.Exceptions;
using MediatR;

namespace Application.Users.Queries;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
  private readonly IUserRepository _userRepository;

  public GetUserByIdQueryHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
  {
    return await _userRepository.GetByIdAsync(request.Id)
      ?? throw new UserNotFoundException("User was not found.");
  }
}
