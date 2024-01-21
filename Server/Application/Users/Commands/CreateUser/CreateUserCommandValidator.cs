using Domain.Users;
using FluentValidation;

namespace Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
  public CreateUserCommandValidator(IUserRepository userRepository)
  {
    RuleFor(c => c.Name).NotEmpty().WithMessage("The name cannot be empty.");

    RuleFor(c => c.Email).EmailAddress().WithMessage("Invalid email address.");
  }
}
