using FluentValidation;
using PatientService.Domain.DTOs;
using PatientService.Domain.Entities;

namespace PatientService.Validators
{
    public class VaccinationValidator: AbstractValidator<VaccinationDTO>
    {
        public VaccinationValidator()
        {
            RuleFor(v => v.VaccineName).NotEmpty().WithMessage("Vaccine name is required.");
            RuleFor(v => v.VaccinationDate).NotEmpty().WithMessage("Vaccination date is required.");
            RuleFor(v => v.DoseNumber).GreaterThan(0).WithMessage("Dose number must be greater than zero.");
        }
    }
}
