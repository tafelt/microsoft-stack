using Domain.Common.SeedWork;

namespace Domain.Users;

public interface IUserRepository : IRepository<User, int>
{
  Task<User?> GetByEmailAsync(string email);
}
