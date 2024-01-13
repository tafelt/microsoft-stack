using Domain.Users;
using Domain.Users.Exceptions;
using MediatR;

namespace Application.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User>
{
  private readonly IUserRepository _userRepository;

  public DeleteUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
  {
    return await _userRepository.DeleteAsync(request.Id)
      ?? throw new UserNotFoundException("User was not found.");
  }
}
