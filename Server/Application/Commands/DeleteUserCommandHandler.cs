using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User?>
{
  private readonly IUserRepository _userRepository;

  public DeleteUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<User?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
  {
    return _userRepository.DeleteAsync(request.Id);
  }
}
