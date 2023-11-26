using Domain.Entities;
using MediatR;

namespace Application.Queries;

public record GetUserAllQuery : IRequest<List<User>>;
