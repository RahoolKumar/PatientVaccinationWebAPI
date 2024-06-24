using FluentValidation;
using PatientService.Domain.DTOs;
using PatientService.Domain.Entities;

namespace PatientService.Validators
{
    public class PatientValidator: AbstractValidator<PatientDTO>
    {
        public PatientValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Invalid email format.").When(p => !string.IsNullOrEmpty(p.Email));
            RuleFor(p => p.PhoneNumber).Matches(@"^\d{10}$").WithMessage("Invalid phone number format.").When(p => !string.IsNullOrEmpty(p.PhoneNumber));
        }
    }
}
