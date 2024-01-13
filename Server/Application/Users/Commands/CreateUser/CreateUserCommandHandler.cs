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
    var existingUser = await _userRepository.GetByEmailAsync(request.Email);

    if (existingUser is not null)
    {
      throw new UserAlreadyExistsException("User already exists.");
    }

    // NOTE: SqlServer automatically generates the ID when the row is inserted
    var user = new User(-999, request.Name, request.Email);

    return await _userRepository.CreateAsync(user);
  }
}
