using Domain.Users;
using Domain.Users.Exceptions;
using MediatR;

namespace Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
  private readonly IUserRepository _userRepository;

  public UpdateUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
  {
    var user = new User(request.Id, request.Name, request.Email);

    return await _userRepository.UpdateAsync(user)
      ?? throw new UserNotFoundException("User was not found.");
  }
}
