using FluentValidation.Validators;

namespace Application.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
                RuleFor(contact => contact.FirstName)
                    .NotEmpty().WithMessage("FirstName is required.")
                    .Length(1, 70).WithMessage("This must be between 1 and 70 characters.");

                RuleFor(contact => contact.LastName)
                    .NotEmpty().WithMessage("LastName is required.")
                    .Length(1, 70).WithMessage("This must be between 1 and 70 characters.");

                RuleFor(contact => contact.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("This is not a valid email.")
                    .Length(1, 40).WithMessage("This must be between 1 and 40 characters.");
        }
    }
}
