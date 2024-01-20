using Application.Users.Commands;
using Contracts.Users;
using Domain.Users;
using Mapster;

namespace API.Mapping;

public class UsersMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<CreateUserRequest, CreateUserCommand>();

    config.NewConfig<User, UserResponse>();
  }
}
