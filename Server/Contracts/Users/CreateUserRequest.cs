using Domain.Users;

namespace Contracts.Users;

public record CreateUserRequest(string Name, string Email, Address Address);
