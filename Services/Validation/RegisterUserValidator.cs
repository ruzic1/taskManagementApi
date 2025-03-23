using FluentValidation;
using TaskManagementAPI.DTO;

namespace TaskManagementAPI.Services.Validation
{
    public class RegisterUserValidator:AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("First Name is required")
                .MinimumLength(3).WithMessage("First Name must be at least 3 characters")
                .MaximumLength(20).WithMessage("First Name must be shorter than 20 characters")
                .Matches(@"^[A-Z][a-z]*$").WithMessage("First name must start with capital letter");


            RuleFor(x => x.LastName)
                .NotNull().WithMessage("Last name is required")
                .MinimumLength(3).WithMessage("Last Name must be at least 3 characters")
                .MaximumLength(20).WithMessage("Last Name must be shorter than 20 characters")
                .Matches(@"^[A-Z][a-z]*$").WithMessage("Last name must start with capital letter");

            RuleFor(x => x.Username)
                .NotNull().WithMessage("Username is required")
                .MinimumLength(4).WithMessage("Username must be at least 4 characters")
                .MaximumLength(20).WithMessage("Username must be shorter than 20 characters")
                .Matches(@"^[A-Za-z0-9_.-]*").WithMessage("Username has invalid value (etc. john_doe123)");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required");
                
                //.Matches(@"")
        }
    }
}
