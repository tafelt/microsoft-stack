using Domain.Common.SeedWork;

namespace Domain.Users;

public class User : Entity<int>, IAggregateRoot
{
  public string Name { get; set; }

  public string Email { get; set; }

  public Address Address { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  public User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

  public User(int id, string name, string email, Address address)
    : base(id)
  {
    Name = name;
    Email = email;
    Address = address;
  }
}
