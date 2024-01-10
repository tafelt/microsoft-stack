using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User?>
{
  private readonly IUserRepository _userRepository;

  public UpdateUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<User?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
  {
    var user = new User(request.Id, request.Name, request.Email);

    return _userRepository.UpdateAsync(user);
  }
}
