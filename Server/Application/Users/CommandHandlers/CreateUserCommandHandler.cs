using Application.Users.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Users.CommandHandlers;

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
