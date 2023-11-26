using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
  private readonly IUserRepository _userRepository;

  public CreateUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var user = new User { Name = request.Name, Email = request.Email };

    return _userRepository.CreateAsync(user);
  }
}
