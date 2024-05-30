using Domain.Users;

namespace Contracts.Users;

public record UserResponse(int Id, string Name, string Email, Address Address);
