using Domain.Entities;
using MediatR;

namespace Application.Users.Queries;

public record GetUserAllQuery : IRequest<IEnumerable<User>>;
