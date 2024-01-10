using Domain.Users;
using MediatR;

namespace Application.Users.Queries;

public record GetAllUsersQuery : IRequest<IEnumerable<User>>;
