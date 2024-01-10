using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
  private readonly IUserRepository _userRepository;

  public CreateUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    // NOTE: SqlServer automatically generates the ID information
    var user = new User(-1, request.Name, request.Email);

    return _userRepository.CreateAsync(user);
  }
}
