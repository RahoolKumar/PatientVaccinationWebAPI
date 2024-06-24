using PatientService.Common;
using PatientService.Domain.DTOs;

namespace PatientService.Repositories.Interfaces
{
    public interface IVaccinationRepository
    {
        Task<AppResponse> AddVaccination(VaccinationDTO vaccinationDTO);
        Task<AppResponse> GetVaccinationById(int id);
        Task<AppResponse> GetVaccinationsByPatientId(int patientId);
        Task<AppResponse> UpdateVacination(VaccinationDTO patient, int id);
        
    }
}
