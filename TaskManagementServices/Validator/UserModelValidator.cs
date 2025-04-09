using FluentValidation;
using TaskManagementServices.DTOs;

namespace TaskManagementServices.Validator
{
    public class UserModelValidator : AbstractValidator<UserCreateModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long");
        }
    }
}
