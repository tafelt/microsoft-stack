using Domain.SeedWork;

namespace Domain.Users;

public class User : Entity<int>, IAggregateRoot
{
  public string Name { get; set; }

  public string Email { get; set; }

  public User(int id, string name, string email)
    : base(id)
  {
    Name = name;
    Email = email;
  }
}
