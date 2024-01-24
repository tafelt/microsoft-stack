using System.Net;
using Domain.Exceptions;

namespace Domain.Users.Exceptions;

public class UserAlreadyExistsException : Exception, IDomainException
{
  public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

  public Type Type => typeof(UserAlreadyExistsException);

  public string Title => "Conflict";

  public string Detail => "User already exists.";

  public UserAlreadyExistsException() { }

  public UserAlreadyExistsException(string message)
    : base(message) { }

  public UserAlreadyExistsException(string message, Exception inner)
    : base(message, inner) { }
}
