using Domain.Common.SeedWork;

namespace Domain.Users;

public class Address : ValueObject
{
  public string Street { get; init; } = string.Empty;

  public string City { get; init; } = string.Empty;

  public string State { get; init; } = string.Empty;

  public string Country { get; init; } = string.Empty;

  public string ZipCode { get; init; } = string.Empty;

  public Address() { }

  public Address(string street, string city, string state, string country, string zipCode)
  {
    Street = street;
    City = city;
    State = state;
    Country = country;
    ZipCode = zipCode;
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return Street;
    yield return City;
    yield return State;
    yield return Country;
    yield return ZipCode;
  }
}
