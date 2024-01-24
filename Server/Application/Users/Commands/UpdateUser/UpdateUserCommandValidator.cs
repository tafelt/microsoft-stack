using FluentValidation;

namespace Application.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
  public UpdateUserCommandValidator()
  {
    RuleFor(c => c.Name).NotEmpty().WithMessage("The name cannot be empty.");

    RuleFor(c => c.Email).EmailAddress().WithMessage("Invalid email address.");
  }
}
