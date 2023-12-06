using Application.Users.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Users.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User?>
{
  private readonly IUserRepository _userRepository;

  public UpdateUserCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Task<User?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
  {
    var user = new User
    {
      Id = request.Id,
      Name = request.Name,
      Email = request.Email
    };

    return _userRepository.UpdateAsync(user);
  }
}
