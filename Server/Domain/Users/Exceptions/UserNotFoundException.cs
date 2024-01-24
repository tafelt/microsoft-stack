using System.Net;
using Domain.Exceptions;

namespace Domain.Users.Exceptions;

public class UserNotFoundException : Exception, IDomainException
{
  public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

  public Type Type => typeof(UserNotFoundException);

  public string Title => "NotFound";

  public string Detail => "User was not found.";

  public UserNotFoundException() { }

  public UserNotFoundException(string message)
    : base(message) { }

  public UserNotFoundException(string message, Exception inner)
    : base(message, inner) { }
}
