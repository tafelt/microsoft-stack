using Application.Users.Commands;
using Domain.Users;
using Mapster;

namespace Contracts.Users;

public class UsersMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<CreateUserRequest, CreateUserCommand>();

    config.NewConfig<User, UserResponse>();
  }
}
