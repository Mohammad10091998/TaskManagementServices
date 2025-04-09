using FluentValidation;
using TaskManagementServices.DTOs;

namespace TaskManagementServices.Validator
{
    public class TaskModelValidator : AbstractValidator<TaskModel>
    {
        public TaskModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Due date must be today or future date");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status");
        }
    }
}
