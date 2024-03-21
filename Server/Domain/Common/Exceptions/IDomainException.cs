using System.Net;

namespace Domain.Common.Exceptions;

public interface IDomainException
{
  HttpStatusCode StatusCode { get; }

  Type Type { get; }

  string Title { get; }

  string Detail { get; }
}
