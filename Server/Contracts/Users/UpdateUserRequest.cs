using Domain.Users;

namespace Contracts.Users;

public record UpdateUserRequest(string Name, string Email, Address Address);
