﻿using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class CreateUserCommand : IRequest<User>
{
  public required string Name { get; set; }

  public required string Email { get; set; }
}
