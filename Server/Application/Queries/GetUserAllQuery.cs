using Domain.Entities;
using MediatR;

namespace Application.Queries;

public record GetUserAllQuery : IRequest<IEnumerable<User>>;
