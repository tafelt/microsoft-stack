using Domain.Users;
using Domain.Users.Exceptions;
using MediatR;

namespace Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
  private readonly IUserRepository _userRepository;

  public CreateUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var userExists = await _userRepository.GetByEmailAsync(request.Email) is not null;

    if (userExists)
    {
      throw new UserAlreadyExistsException();
    }

    // NOTE: SqlServer automatically generates the ID when the row is inserted
    var user = new User(-999, request.Name, request.Email, request.Address);

    return await _userRepository.CreateAsync(user);
  }
}
