using System.Net;

namespace Domain.Exceptions;

public interface IDomainException
{
  HttpStatusCode StatusCode { get; }

  Type Type { get; }

  string Title { get; }

  string Detail { get; }
}
